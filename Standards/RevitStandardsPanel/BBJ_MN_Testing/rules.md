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
      "Rule Name": "Mark is Number",
      "User Message": "Mark must be a number",
      "Categories": [
        "Casework"
      ],
      "Parameter Name": "Mark",
      "Regex": "^[0-9]+$|^$"
    },
    {
    "Rule Name": "Family Name is Compliant",
    "User Message": "Family name must be compliant with BBJ Standards",
    "Categories": ["Doors"],
    "Parameter Name": "Mark",
    "When Run": ["Manual"],
    "Disable By Default": ["True"],
    "Regex": "^08_.*"
    }
  ]
}
```
