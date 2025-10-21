using System;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace IDC.Rules
{
    public class PreventFunctNumDups
    {
        public static void Execute(Document doc, ElementId changedElementId)
        {
            try
            {
                Element changedElem = doc.GetElement(changedElementId);
                if (changedElem == null)
                    return;

                // Only run on Rooms
                if (!(changedElem is SpatialElement room) || room.Category == null || room.Category.Name != "Rooms")
                    return;

                // Get the Function Number parameter
                Parameter funcNumParam = room.LookupParameter("dRofus_FunctionNumber");
                if (funcNumParam == null)
                    return;

                string funcNum = funcNumParam.AsString();
                if (string.IsNullOrWhiteSpace(funcNum))
                    return; // No value, nothing to check

                // Collect all other rooms in the document
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

                // If duplicate found, clear the value
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
