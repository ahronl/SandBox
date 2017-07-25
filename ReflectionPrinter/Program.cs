using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionPrinter
{
    class Program
    {
        static void Main(string[] args)
        {
            Person man = CreatePerson();

            string desc = ConvertToString(man);

            Console.WriteLine(desc);
            Console.ReadKey();
            /*
             print 
                Adam Age: 100 
                    Son 1 Age: 1
                    Son 2 Age: 2
                    Son 3 Age: 3
                    Son 4 Age: 4
                    Son 5 Age: 5.......
             
             */
        }

        private static Person CreatePerson()
        {
            Person man = new Person("Adam", 100);

            for (int j = 1; j < 5 + 1; j++)
            {
                var child = new Person($"son number {j}", 100 - j);
                man.Children.Add(child);

                for (int i = 1; i < 5; i++)
                {
                    var grandson = new Person($"grandson number {i}", 100 - i - j);
                    child.Children.Add(grandson);

                    for (int k = 1; k < 5; k++)
                    {
                        grandson.Children.Add(new Person($"great grandson number {i}", 100 - i - j - k));
                    }
                }
            }
            return man;
        }
        private static string ConvertToString(object obj, int tabcount = 0)
        {
            string res = string.Empty;

            Type type = obj.GetType();

            foreach (var propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType.IsEquivalentTo(typeof(string)))
                {
                    res += $" {propertyInfo.Name} : {propertyInfo.GetValue(obj, null)} ";
                }
                else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    IEnumerable<object> items = propertyInfo.GetValue(obj, null) as IEnumerable<object>;

                    if(items == null) return String.Empty;

                    tabcount++;

                    foreach (var subObj in items)
                    {
                        res += Environment.NewLine + string.Concat(Enumerable.Repeat("\t",tabcount)) + ConvertToString(subObj, tabcount);
                    }
                }
                else if (propertyInfo.PropertyType.IsClass)
                {
                    res += Environment.NewLine +  ConvertToString(propertyInfo.GetValue(obj));
                }
            }

            return res;
        }
    }
}
