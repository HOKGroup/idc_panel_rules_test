using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

public class InPlaceFamilyCheck
{
    public IEnumerable<ElementId> Run(Document doc, List<ElementId> ids)
    {
        // Defensive check
        if (ids == null || ids.Count == 0)
            return null;

        // Filter for FamilyInstances in the modified elements
        var inplaceFamilies = ids
            .Select(id => doc.GetElement(id) as FamilyInstance)
            .Where(inst => inst != null && inst.Symbol != null && inst.Symbol.Family != null && inst.Symbol.Family.IsInPlace)
            .ToList();

        // Only flag if model now has 25 or more in-place families
        // We do a one-time count to avoid triggering violations for single edits in isolation
        int totalInPlaceCount = new FilteredElementCollector(doc)
            .OfClass(typeof(FamilyInstance))
            .Cast<FamilyInstance>()
            .Count(fi => fi.Symbol.Family.IsInPlace);

        if (totalInPlaceCount < 25)
            return null;

        return inplaceFamilies.Select(inst => inst.Id);
    }
}
