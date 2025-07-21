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
      "Rule Name": "Area Non-Negative When dRofus Function Number Set",
      "Categories": ["Rooms"],
      "Parameter Name": "Area",
      "Requirement": "IF {dRofus_FunctionNumber} != \"\" THEN {Area} >= 0",
      "User Message": "Area must be zero or greater when dRofus Function Number is set"
    }
]
}
```
