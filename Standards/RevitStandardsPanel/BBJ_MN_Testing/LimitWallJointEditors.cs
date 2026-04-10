// RestrictedCategoryEditor.cs
// Place in the same folder as rules.md
// Blocks edits to elements of a specified category by anyone not on the allowed-editors list.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autodesk.Revit.DB;

public class RestrictedCategoryEditor
{
    // ---------------------------------------------------------------
    // TESTING TOGGLE
    // Set USE_HARDCODED_USER = true to bypass the CSV and test with
    // the username below. Flip back to false for production.
    // ---------------------------------------------------------------
    private const bool USE_HARDCODED_USER = true;
    private const string HARDCODED_TEST_USER = "t.lane"; // ← your Autodesk login

    // ---------------------------------------------------------------
    // Configuration
    // ---------------------------------------------------------------
    private const string RestrictedFamilyName = "Masonry Control Joint";
    private static readonly BuiltInCategory RestrictedCategory = BuiltInCategory.OST_ExpansionJoints;
    private const string AllowlistFileName = "RestrictedCategoryEditorAllowlist.csv";

    // ---------------------------------------------------------------

    public IEnumerable<ElementId> Run(Document doc, List<ElementId> ids)
    {
        // 1. Build the allowlist — either hardcoded or from CSV.
        var allowlist = USE_HARDCODED_USER
            ? new HashSet<string>(StringComparer.OrdinalIgnoreCase) { HARDCODED_TEST_USER.Trim().ToLowerInvariant() }
            : LoadAllowlist(doc);

        // 2. Get the current Autodesk user login name.
        var currentUser = (doc.Application.Username ?? string.Empty).Trim().ToLowerInvariant();

        // 3. If the current user is on the allowlist, nothing to flag.
        if (allowlist.Contains(currentUser))
            return Enumerable.Empty<ElementId>();

        // 4. Determine which changed elements belong to the restricted family.
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

            // Must be in the correct category AND belong to the restricted family.
            if (element.Category == null ||
                element.Category.Id.IntegerValue != (int)RestrictedCategory)
                continue;

            var fi = element as FamilyInstance;
            if (fi?.Symbol?.FamilyName == RestrictedFamilyName)
                violating.Add(id);
        }

        return violating;
    }

    // ------------------------------------------------------------------
    // Helpers
    // ------------------------------------------------------------------

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
