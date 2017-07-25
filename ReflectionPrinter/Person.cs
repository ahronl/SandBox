using System.Collections.Generic;

namespace ReflectionPrinter
{
    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public List<Person> Children { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
            Children = new List<Person>();
        }
    }
}