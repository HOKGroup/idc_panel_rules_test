# Sample Rule Documentation

```json
{
"Workset Rules":
[
    {
        "Categories": ["Furniture", "Entourage"],
        "Workset": "Level 1 Stuff",
        "Parameters":
        [
            {"Name": "Level", "Value": "Level 1"},
            {"Name": "Workset Rule Applies", "Value": "1"}
        ],
      "When Run": ["Manual"],
      "Disable By Default": "true"
    },
        {
        "Categories": ["Furniture", "Entourage"],
        "Workset": "Level 2 Stuff",
        "Parameters":
        [
            {"Name": "Level", "Value": "Level 2"},
            {"Name": "Workset Rule Applies", "Value": "1"}
        ],
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
    },
    {
      "Rule Name": "Mark is Number",
      "User Message": "Mark must be a number",
      "Categories": [
        "<all>"
      ],
      "Disable By Default": "true",
      "Parameter Name": "Mark",
      "Regex": "^[0-9]+$"
    },
  ]
}
```
