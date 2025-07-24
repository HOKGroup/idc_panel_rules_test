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
      "Rule Name": "Hello World",
      "Element Classes": ["Project Info"],
      "Custom Code": "HelloWorld",
      "User Message": "Hello World!",
      "When Run": ["Manual", "Open"]
    }
]
}
```
