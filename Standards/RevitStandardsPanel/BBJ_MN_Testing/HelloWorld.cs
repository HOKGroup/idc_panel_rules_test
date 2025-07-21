using System.Collections.Generic;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

public class HelloWorld
{
    public IEnumerable<ElementId> Run(Document doc, List<ElementId> ids)
    {
        new TaskDialog("Hello World").Show();
        return null;
    }
}
