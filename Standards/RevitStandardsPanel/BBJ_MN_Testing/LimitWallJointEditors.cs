// RestrictedCategoryEditor.cs
// Place in the same folder as rules.md
// Blocks edits to elements of a specified category by anyone not on the allowed-editors list.
// The allowed usernames are read from a companion CSV: RestrictedCategoryEditorAllowlist.csv

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autodesk.Revit.DB;

public class RestrictedCategoryEditor
{
    // ---------------------------------------------------------------
    // Configuration — adjust these two values to match your use case.
    // ---------------------------------------------------------------

    // Revit built-in category you want to restrict.
    // Common examples:
    //   BuiltInCategory.OST_Walls
    //   BuiltInCategory.OST_StructuralColumns
    //   BuiltInCategory.OST_MEPSpaces
    //   BuiltInCategory.OST_Rooms
    private static readonly BuiltInCategory RestrictedCategory = BuiltInCategory.OST_Walls;

    // Name of the CSV file (one Autodesk username per line, no header)
    // that lives alongside this .cs file in the standards folder.
    private const string AllowlistFileName = "RestrictedCategoryEditorAllowlist.csv";

    // ---------------------------------------------------------------

    public IEnumerable<ElementId> Run(Document doc, List<ElementId> ids)
    {
        // 1. Resolve the allowlist file path relative to this script's location.
        //    The addin compiles .cs files from the standards folder, so we use
        //    the document path as an anchor when running in "real-time" mode,
        //    and fall back to a path beside the temp-compiled assembly.
        var allowlist = LoadAllowlist(doc);

        // 2. Get the current Autodesk user login name (lowercase for comparison).
        var currentUser = (doc.Application.Username ?? string.Empty).Trim().ToLowerInvariant();

        // 3. If the current user is on the allowlist, nothing to flag.
        if (allowlist.Contains(currentUser))
            return Enumerable.Empty<ElementId>();

        // 4. Determine which of the changed elements belong to the restricted category.
        var violating = new List<ElementId>();

        // When ids is null the rule was triggered via the "Run" button — check all.
        var idsToCheck = ids ?? new FilteredElementCollector(doc)
                                    .OfCategory(RestrictedCategory)
                                    .WhereElementIsNotElementType()
                                    .ToElementIds()
                                    .ToList();

        foreach (var id in idsToCheck)
        {
            var element = doc.GetElement(id);
            if (element == null) continue;

            // Match by built-in category.
            if (element.Category != null &&
                element.Category.Id.IntegerValue == (int)RestrictedCategory)
            {
                violating.Add(id);
            }
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
        // The addin compiles custom .cs files into a temp location, but it passes
        // the Document so we can navigate from the model's own path.  A reliable
        // fallback is to look next to the executing assembly — adjust if your
        // deployment differs.
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

        // Fallback: same directory as the Revit model file.
        if (!string.IsNullOrEmpty(doc.PathName))
            return Path.GetDirectoryName(doc.PathName) ?? string.Empty;

        return string.Empty;
    }
}
