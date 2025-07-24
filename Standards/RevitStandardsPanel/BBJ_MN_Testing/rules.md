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
      "Rule Name": "Comments a b c",
      "Categories": [
        "Walls"
      ],
      "Parameter Name": "Comments",
      "User Message": "Comments must be a, b, or c",
      "List Options":
      [
        {"name": "a", "description": ""},
        {"name": "b", "description": ""},
        {"name": "c", "description": ""}
      ],
      "When Run": ["Manual"]
    }
  ]
}
