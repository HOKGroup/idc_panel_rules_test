# Sample Rule Documentation

```json
{
  "Parameter Rules": 
  [
    {
      "Rule Name": "Hello World",
      "Element Classes": [
        "Autodesk.Revit.DB.FamilyInstance"
      ],
      "Custom Code": "HelloWorld"
    },
    {
      "Rule Name": "In Place Family Quantity",
      "Element Classes": [
        "Autodesk.Revit.DB.FamilyInstance"
      ],
      "Custom Code": "InPlaceFamilyCheck",
      "User Message": "There are too many In-Place Families in the model."
    }
  ]
}
```
