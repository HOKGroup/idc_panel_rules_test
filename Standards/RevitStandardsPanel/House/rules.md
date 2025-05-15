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
        "Disable By Default": "true",
        "When Run": ["Manual"]
    },
        {
        "Categories": ["Furniture", "Entourage"],
        "Workset": "Level 2 Stuff",
        "Parameters":
        [
            {"Name": "Level", "Value": "Level 2"},
            {"Name": "Workset Rule Applies", "Value": "1"}
        ],
        "Disable By Default": "true",
        "When Run": ["Manual"]
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
```
