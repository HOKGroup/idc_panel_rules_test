# Sample Rule Documentation

```json
{
  "Workset Rules":
  [
    {
      "Categories": ["Furniture", "Entourage"],
      "Workset": "Level 1 Fitout",
      "Parameters":
      [
        {"Name": "Level", "Value": "01 - Entry Level"},
        {"Name": "Auto Assign Workset", "Value": "1"}
      ]
    },
    {
      "Categories": ["Furniture", "Entourage"],
      "Workset": "Level 2 Fitout",
      "Parameters":
      [
        {"Name": "Level", "Value": "02 - Floor"},
        {"Name": "Auto Assign Workset", "Value": "1"}
      ]
    }
  ],
  "Parameter Rules": 
  [
  {
    "Rule Name": "Set Wall Type Function",
      "Element Classes": [
        "Autodesk.Revit.DB.WallType"
      ],
      "Parameter Name": "Type Name",
      "Format": "{Function} - {Structural Material} - {Width}",
      "User Message": "Type name does not match required format"
  },
  {
    "Rule Name": "Set Room Style",
      "Categories": ["Rooms"],
      "Parameter Name": "Room Style",
      "Driven Parameters": ["Wall Finish", "Floor Finish", "Ceiling Finish"],
      "Key Values": [
        ["A", "Wall A", "Floor A", "Ceiling A"],
        ["B", "Wall B", "Floor B", "Ceiling B"],
        ["C", "Wall C", "Floor C", "Ceiling C"]
        ]
    },
      {
    "Rule Name": "Set Room Occupant",
      "Categories": ["Rooms"],
      "Parameter Name": "Occupant",
      "Driven Parameters": ["OccupantLoadFactor","Occupant_General", "Occupant_Specific_Use"],
      "Key Values": [
        ["Assembly_Excercise_with_Equip_TX_21", "50", "Assembly", "Excercise with Equipment"],
        ["Assembly_Waiting_TX_21", "5", "Assembly", "Waiting"],
        ["Business_TX_21", "50", "Business", ""],
        ["Storage_TX_21", "40", "Storage", ""],
        ["Assembly_Excercise_without_Equip_TX_21", "15", "Assembly", "Excercise without Equipment"],
        ]
    },
    {
      "Rule Name": "Room Occupannt Load Calculation",
      "Categories": ["Rooms"],
      "Parameter Name": "OccupantLoad",
      "Formula": "{Area}/{OccupantLoadFactor}"
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
      "Rule Name": "Insert Orientation = Host Insert",
      "Categories": [
        "Doors", "Windows"
      ],
      "Parameter Name": "Orientation",
      "From Host Instance": "Orientation",
      "User Message": "The Orientation of an insert must equal the Orientation of its host"
    },
    {
      "Rule Name": "Room Number Dup",
      "Categories": [
        "Rooms"
      ],
      "User Message": "Room Number cannot duplicate an existing value",
      "Parameter Name": "Number",
      "Prevent Duplicates": "True"
    }
  ]
}
```
