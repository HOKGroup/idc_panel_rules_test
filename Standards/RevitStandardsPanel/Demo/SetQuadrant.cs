using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Autodesk.Revit.DB;

public class SetQuadrant
{
	public void Run(Document doc)
	{
		var instances = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance));
		var scopeboxes = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_VolumeOfInterest);
		foreach (var scopebox in scopeboxes)
		{
			var box = scopebox.get_BoundingBox(null);
			var outline = new Outline(box.Min, box.Max);
			var elementsInside = new FilteredElementCollector(doc, instances.Select(q => q.Id).ToList())
				.WherePasses(new BoundingBoxIntersectsFilter(outline)).ToElements();
			foreach (var element in elementsInside)
			{
				var parameter = element.LookupParameter("Quadrant");
				if (parameter == null)
				{
					continue;
				}
				parameter.Set(scopebox.Name);
			}
		}
	}
}