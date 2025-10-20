using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

public class SheetGroupRule
{
    public IEnumerable<ElementId> Run(Document doc, List<ElementId> ids)
    {
        // Get sheets either from ids or all sheets
        List<ViewSheet> sheets = (ids == null)
            ? new FilteredElementCollector(doc).OfClass(typeof(ViewSheet)).Cast<ViewSheet>().ToList()
            : ids.Select(q => doc.GetElement(q)).OfType<ViewSheet>().ToList();

        foreach (var sheet in sheets)
        {
            var groupParam = sheet.LookupParameter("Sheet Group");
            if (groupParam == null) continue;

            string groupValue = groupParam.AsString();
            if (groupValue == "_COORDINATION" || groupValue == "_PRESENTATION")
            {
                var appearsInSheetListParam = sheet.LookupParameter("Appears In Sheet List");
                if (appearsInSheetListParam != null && appearsInSheetListParam.HasValue)
                {
                    appearsInSheetListParam.Set(0); // Uncheck / set to false
                }
            }
        }

        return null;
    }
}
