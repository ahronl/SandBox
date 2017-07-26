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
            string res = string.Empty + Environment.NewLine;

            Type type = obj.GetType();

            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                res += ConvertToPropertyToString(propertyInfo, obj, tabcount);
            }

            return res;
        }

        private static string ConvertToPropertyToString(PropertyInfo propertyInfo, object obj, int tabcount)
        {
            if (IsValueTypeOrString(propertyInfo))
            {
                return ConvertSimpleTypesToString(propertyInfo, obj, tabcount);
            }
            if (IsEnumerable(propertyInfo))
            {
                return ConvertEnumerableToString(propertyInfo, obj, tabcount);
            }
            if (IsObject(propertyInfo))
            {
                return AppendTabs(tabcount) + ConvertToString(propertyInfo.GetValue(obj, null), tabcount);
            }
            return string.Empty;
        }

        private static string AppendTabs(int tabcount)
        {
            return string.Concat(Enumerable.Repeat("\t", tabcount));
        }

        private static bool IsValueTypeOrString(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType.IsEquivalentTo(typeof(string));
        }

        private static string ConvertSimpleTypesToString(PropertyInfo propertyInfo, object obj,int tabcount)
        {
            return AppendTabs(tabcount) + $" {propertyInfo.Name} : {propertyInfo.GetValue(obj, null)} ";
        }

        private static bool IsEnumerable(PropertyInfo propertyInfo)
        {
            return typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType);
        }

        private static string ConvertEnumerableToString(PropertyInfo propertyInfo, object obj, int tabcount)
        {
            IEnumerable<object> items = propertyInfo.GetValue(obj, null) as IEnumerable<object>;

            if (items == null) return String.Empty;

            tabcount++;

            string res = string.Empty;

            foreach (object subObj in items)
            {
                res += AppendTabs(tabcount) + ConvertToString(subObj, tabcount);
            }
            return res;
        }

        private static bool IsObject(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.IsClass;
        }
    }
}
