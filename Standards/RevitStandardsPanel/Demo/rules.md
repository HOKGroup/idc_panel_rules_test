# Sample Rule Documentation

```json
{
  "Workset Rules":
  [
    {
      "Categories": ["Rooms", "Furniture"],
      "Workset": "Level 1 - Roooms and Furniture",
      "Parameters":
      [
        {"Name": "Level", "Value": "Level 1"},
        {"Name": "Auto Assign Workset", "Value": "1"}
      ]
    },
    {
      "Categories": ["Walls"],
      "Workset": "Level 1 Walls and Doors",
      "Parameters":
      [
        {"Name": "Base Constraint", "Value": "Level 1"},
        {"Name": "Auto Assign Workset", "Value": "1"}
      ]
    },
        {
      "Categories": ["Doors"],
      "Workset": "Level 1 Walls and Doors",
      "Parameters":
      [
        {"Name": "Level", "Value": "Level 1"},
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
      "Format": "{HOK Partition Series}_{HOK Wall Structure}_{Fire Rating}_{HOK Wall Sheathing}_{HOK Wall Description}",
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
      "Rule Name": "Host Fire Rating",
      "Categories": [
        "Doors", "Windows"
      ],
      "Parameter Name": "HostFireRating",
      "From Host Instance": "FireRating",
      "User Message": "Fire Rating Must Match Host"
    },
    {
      "Rule Name": "Set Quadrant",
      "Element Classes": [
        "Autodesk.Revit.DB.FamilyInstance"
      ],
      "Custom Code": "SetQuadrant",
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
