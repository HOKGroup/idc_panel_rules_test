using System.Collections.Generic;
using Autodesk.Revit.DB;

public class InPlaceFamilyCheck
{
    public IEnumerable<ElementId> Run(Document doc)
    {
        List<ElementId> result = new List<ElementId>();
        int count = 0;

        FilteredElementCollector collector = new FilteredElementCollector(doc);
        collector.OfClass(typeof(FamilyInstance));

        foreach (Element element in collector)
        {
            FamilyInstance instance = element as FamilyInstance;
            if (instance != null && instance.Symbol.Family.IsInPlace)
            {
                count++;
                result.Add(instance.Id);
            }
        }

        if (count < 5)
            return null;
        else
            return result;
    }
}
