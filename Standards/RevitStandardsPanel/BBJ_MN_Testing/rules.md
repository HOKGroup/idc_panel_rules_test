# Sample Rule Documentation

```json
{
  "Workset Rules":
  [
    {
      "Categories": ["Levels", "Grids"],
      "Workset": "1 - Shared Views, Levels, Grids",
      "Parameters": [],
      "When Run": ["Save", "SyncToCentral"]
    },
    {
      "Categories": ["Scope Boxes"],
      "Workset": "1 - Scope Boxes",
      "Parameters": [],
      "When Run": ["Save", "SyncToCentral"]
    }

  ],
  "Parameter Rules": 
  [
    {
      "Rule Name": "Comments Rule For All Other Files",
      "Categories": [
        "Walls"
      ],
      "Parameter Name": "Comments",
      "User Message": "Comments must be 1 2 or 3",
      "List Options":
      [
        {"name": "1", "description": ""},
        {"name": "2", "description": ""},
        {"name": "3", "description": ""}
      ]
    },
    {
      "Rule Name": "Conforming Family Name",
      "User Message": "Family Name must conform to project standard (BBJ_MN_A_HOK_CATEGORY_NAME_DESCRIPTION_HOST TYPE)",
      "Categories": ["Casework", "Furniture"],
      "Parameter Name": "Family Name",
      "Regex": "^BBJ_MN_A_HOK_.+",
      "When Run": ["Realtime", "Manual"]
    },
    {
      "Rule Name": "Room dRofus_FunctionNumber Dup",
      "Categories": ["Rooms"],
      "User Message": "dRoufs Function Number cannot duplicate an existing value.  Make up a unique value.",
      "Parameter Name": "dRofus_FunctionNumber",
      "Prevent Duplicates": "True"
    },
    {
      "Rule Name": "In Place Family Quantity",
      "Element Classes": [
        "Autodesk.Revit.DB.FamilyInstance"
      ],
      "Custom Code": "InPlaceFamilyCheck",
      "When Run": ["SyncToCentral", "Save"],
      "User Message": "There are too many In-Place Families in the model."
    },
    {
      "Rule Name": "LimitGroupEditors",
      "Element Classes": [
        "Autodesk.Revit.DB.Group"
      ],
      "Custom Code": "LockedGroupEditCheck",
      "When Run": ["SyncToCentral", "Save"],
      "User Message": "You may not edit this group. Contact the owner of this group for changes."
    },
    {
      "Rule Name": "Non-Documentation Sheets",
      "Element Classes": ["Autodesk.Revit.DB.ViewSheet"],
      "Custom Code": "NonDocumentationSheets",
      "User Message": "Include in Print Set unchecked if Sheet Group set to _COORDINATION"
    }
  ]
}
```
