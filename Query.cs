using System.Collections;
using System.Collections.Generic;

namespace IssueNullViolation
{
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
}
