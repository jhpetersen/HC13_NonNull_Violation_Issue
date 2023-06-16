# HC13_NonNull_Violation_Issue
Minimal Repo to demonstrate possible issue with HotChocolate 13.2.1 non null violation handling

Guided by the Video from Michael https://youtu.be/Zx0nvTUfjn4 on Non-Nullability and Error Handling, i would expect that

    public class Query
    {
        public Person GetPerson() => new("Luke Skywalker")
        {
            Children = new List<Person>()
            {
                new("Anakin Skywalker"),
                // why does HC 13.2.1 null out children field when a child is null
                // but the response doesn't contain an error property??!
                null
            }
        };
    }

    public class Person
    {
        public Person(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public ICollection<Person>? Children { get; set; }
    }
 
results in a response with partial valid data (children field nulled becaus it contains an invalid null value) *and* an error node.
But the produced response does only contain the partial valid data, but no error hint
  
    {
      "data": {
        "person": {
          "name": "Luke Skywalker",
          "children": null
        }
      }
    }
  
Has something changed since the video was made?
Do we have to opt-in to error information now?
