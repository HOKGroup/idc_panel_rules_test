// LimitWallJointEditors.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autodesk.Revit.DB;

public class ElementEditorRestriction_MasonryControlJoint  // ← matches filename
{
    // ---------------------------------------------------------------
    // TESTING TOGGLE
    // ---------------------------------------------------------------
    private const bool USE_HARDCODED_USER = false;
    private const string HARDCODED_TEST_USER = "t.lane";

    // ---------------------------------------------------------------
    // Configuration
    // ---------------------------------------------------------------
    private const string RestrictedFamilyName = "Masonry Control Joint";
    private static readonly BuiltInCategory RestrictedCategory = BuiltInCategory.OST_ExpansionJoints;
    private const string AllowlistFileName = "ElementEditorRestriction_MasonryControlJoints_Editors.csv";

    // ---------------------------------------------------------------

    public IEnumerable<ElementId> Run(Document doc, List<ElementId> ids)
    {
        var allowlist = USE_HARDCODED_USER
            ? new HashSet<string>(StringComparer.OrdinalIgnoreCase) { HARDCODED_TEST_USER.Trim().ToLowerInvariant() }
            : LoadAllowlist(doc);

        var currentUser = (doc.Application.Username ?? string.Empty).Trim().ToLowerInvariant();

        if (allowlist.Contains(currentUser))
            return Enumerable.Empty<ElementId>();

        var violating = new List<ElementId>();

        var idsToCheck = ids ?? new FilteredElementCollector(doc)
                                    .OfCategory(RestrictedCategory)
                                    .WhereElementIsNotElementType()
                                    .ToElementIds()
                                    .ToList();

        foreach (var id in idsToCheck)
        {
            var element = doc.GetElement(id);
            if (element == null) continue;

            if (element.Category == null ||
                element.Category.Id.IntegerValue != (int)RestrictedCategory)
                continue;

            var fi = element as FamilyInstance;
            if (fi?.Symbol?.FamilyName == RestrictedFamilyName)
                violating.Add(id);
        }

        return violating;
    }

    private static HashSet<string> LoadAllowlist(Document doc)
    {
        var folder = ResolveStandardsFolder(doc);
        var path   = Path.Combine(folder, AllowlistFileName);

        if (!File.Exists(path))
            return new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        return new HashSet<string>(
            File.ReadAllLines(path)
                .Select(l => l.Trim().ToLowerInvariant())
                .Where(l => !string.IsNullOrEmpty(l)),
            StringComparer.OrdinalIgnoreCase);
    }

    private static string ResolveStandardsFolder(Document doc)
    {
        try
        {
            var assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            if (!string.IsNullOrEmpty(assemblyPath))
            {
                var dir = Path.GetDirectoryName(assemblyPath);
                if (dir != null && File.Exists(Path.Combine(dir, AllowlistFileName)))
                    return dir;
            }
        }
        catch { /* ignore */ }

        if (!string.IsNullOrEmpty(doc.PathName))
            return Path.GetDirectoryName(doc.PathName) ?? string.Empty;

        return string.Empty;
    }
}
