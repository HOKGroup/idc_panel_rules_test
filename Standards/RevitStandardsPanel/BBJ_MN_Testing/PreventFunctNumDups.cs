using System;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace IDC.Rules
{
    // Make sure class is public
    public class PreventFunctNumDups : IRule  // <-- implement the expected interface
    {
        public void Execute(Document doc, ElementId changedElementId)
        {
            try
            {
                Element changedElem = doc.GetElement(changedElementId);
                if (changedElem == null)
                    return;

                if (!(changedElem is SpatialElement room) || room.Category == null || room.Category.Name != "Rooms")
                    return;

                Parameter funcNumParam = room.LookupParameter("dRofus_FunctionNumber");
                if (funcNumParam == null)
                    return;

                string funcNum = funcNumParam.AsString();
                if (string.IsNullOrWhiteSpace(funcNum))
                    return;

                FilteredElementCollector collector = new FilteredElementCollector(doc)
                    .OfCategory(BuiltInCategory.OST_Rooms)
                    .WhereElementIsNotElementType();

                bool duplicateFound = collector
                    .Cast<SpatialElement>()
                    .Where(r => r.Id != room.Id)
                    .Any(r =>
                    {
                        Parameter p = r.LookupParameter("dRofus_FunctionNumber");
                        return p != null && string.Equals(p.AsString(), funcNum, StringComparison.OrdinalIgnoreCase);
                    });

                if (duplicateFound)
                {
                    using (Transaction tx = new Transaction(doc, "Clear duplicate Function Number"))
                    {
                        tx.Start();
                        funcNumParam.Set(string.Empty);
                        tx.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show("PreventFunctNumDups", $"Error: {ex.Message}");
            }
        }
    }
}
