using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

public class LockedGroupEditCheck
{
    private static readonly HashSet<string> AllowedUsers = new HashSet<string>
    {
        "trey.lane",
        "ratnamanjiri.shetye",
        "andrew.leach",
        "nel.ramos"
    };

    public IEnumerable<ElementId> Run(Document doc, List<ElementId> ids)
    {
        if (ids == null || ids.Count == 0)
            return null;

        string currentUser = doc.Application.Username.ToLower();

        // Allow listed users bypass enforcement
        if (AllowedUsers.Contains(currentUser))
            return null;

        // Filter modified elements for Groups with "LOCK" in the name
        var lockedGroups = ids
            .Select(id => doc.GetElement(id) as Group)
            .Where(g => g != null && g.GroupType != null)
            .Where(g => g.GroupType.Name != null && g.GroupType.Name.ToUpper().Contains("LOCK"))
            .ToList();

        if (!lockedGroups.Any())
            return null;

        return lockedGroups.Select(g => g.Id);
    }
}
