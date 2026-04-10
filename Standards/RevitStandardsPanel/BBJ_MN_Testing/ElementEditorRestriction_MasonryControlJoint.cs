// ElementEditorRestriction_MasonryControlJoint.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autodesk.Revit.DB;
public class ElementEditorRestriction_MasonryControlJoint
{
    // ---------------------------------------------------------------
    // TESTING TOGGLE
    // Set USE_HARDCODED_USER = true to bypass the CSV and test with
    // the username below. Flip to false to use the CSV allowlist.
    // ---------------------------------------------------------------
    private const bool USE_HARDCODED_USER = false;
    private const string HARDCODED_TEST_USER = "t.lane";

    // ---------------------------------------------------------------
    // DEBUG TOGGLE
    // Set SHOW_DEBUG = true to display a dialog with allowlist and
    // user matching info. Flip to false once everything is working.
    // ---------------------------------------------------------------
    private const bool SHOW_DEBUG = true;

    // ---------------------------------------------------------------
    // Configuration
    // ---------------------------------------------------------------
    private const string RestrictedFamilyName = "Masonry Control Joint";
    private static readonly BuiltInCategory RestrictedCategory = BuiltInCategory.OST_ExpansionJoints;
    private const string AllowlistFileName = "ElementEditorRestriction_MasonryControlJoints_Editors.csv";
    // ---------------------------------------------------------------
    public IEnumerable<ElementId> Run(Document doc, List<ElementId> ids)
    {
        // 1. Build the allowlist — either hardcoded or from CSV.
        var allowlist = USE_HARDCODED_USER
            ? new HashSet<string>(StringComparer.OrdinalIgnoreCase) { HARDCODED_TEST_USER.Trim().ToLowerInvariant() }
            : LoadAllowlist(doc);

        // 2. Get the current Autodesk user login name.
        var currentUser = (doc.Application.Username ?? string.Empty).Trim().ToLowerInvariant();

        // 3. Show debug dialog if enabled.
        if (SHOW_DEBUG)
        {
            var debugFolder = ResolveStandardsFolder(doc);
            var debugList = allowlist.Any()
                ? string.Join(", ", allowlist.Select(u => $"'{u}'"))
                : "(empty — CSV not found or no entries)";
            Autodesk.Revit.UI.TaskDialog.Show("ElementEditorRestriction_MasonryControlJoint Debug",
                $"Mode: {(USE_HARDCODED_USER ? "Hardcoded user" : "CSV allowlist")}\n" +
                $"CSV Folder: {debugFolder}\n" +
                $"Users loaded: {debugList}\n" +
                $"Current user: '{currentUser}'\n" +
                $"Match found: {allowlist.Contains(currentUser)}");
        }

        // 4. If the current user is on the allowlist, nothing to flag.
        if (allowlist.Contains(currentUser))
            return Enumerable.Empty<ElementId>();

        // 5. Determine which changed elements belong to the restricted family.
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
