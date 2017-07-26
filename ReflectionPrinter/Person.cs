using System.Collections.Generic;

namespace ReflectionPrinter
{
    public class Age
    {
        public int MyAge { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }

        public Age Age { get; set; }

        public List<Person> Children { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = new Age() {MyAge = age};
            Children = new List<Person>();
        }
    }
}