# Sample Rule Documentation

```json
{
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
      "Rule Name": "In Place Family Quantity",
      "Element Classes": [
        "Autodesk.Revit.DB.FamilyInstance"
      ],
      "Custom Code": "InPlaceFamilyCheck",
      "User Message": "There are too many In-Place Families in the model."
    },
    {
      "Rule Name": "Set Quadrant",
      "Element Classes": [
        "Autodesk.Revit.DB.FamilyInstance"
      ],
      "Custom Code": "SetQuadrant",
    }
  ]
}
```
