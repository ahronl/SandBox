using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ArraysAndStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            //strings has all unique characters
            //UniqueCharacters();

            //reverse string
            //Reverse();

            //remove duplicate
            RemoveDuplicate();

            Console.ReadKey();
        }

        private static void RemoveDuplicate()
        {
            List<char[]> input = new List<char[]>();
            input.Add(null);
            input.Add(new[] {'a'});
            input.Add(new[] {'a', 'b'});
            input.Add(new[] {'a', 'b', 'a'});
            input.Add(new[] {'a', 'a', 'b'});

            foreach (char[] str in input)
            {
                RemoveDuplicateAndPrint(str);
            }
        }

        private static void RemoveDuplicateAndPrint(char[] str)
        {
            Console.WriteLine("string : {0} no duplicate : {1}", Print(str), Print(RemoveDuplicate(str)));
        }

        private static char[] RemoveDuplicate(char[] str)
        {
            if (str == null) return null;

            if (str.Length == 1) return str;

            for (int i = 0; i < str.Length; i++)
            {
                char item = str[i];

                for (int j = 0; j < str.Length; j++)
                {
                    if (i != j && item == str[j])
                    {
                        str[j] = '*';
                    }
                }
            }

            return str;
        }

        private static void Reverse()
        {
            char[] str = new[] {'a', 'b', 'c', 'd', 'e', 'f', 'g'};
            Console.WriteLine("string : {0} reverse : {1}", Print(str), Print(Reverse(str)));
        }

        private static string Print(char[] str)
        {
            if (str == null) return string.Empty;

            return string.Join("", str);
        }

        private static char[] Reverse(char[] str)
        {
            char[] reversed = new char[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                reversed[str.Length - 1 - i] = str[i];
            }

            return reversed;
        }

        private static void UniqueCharacters()
        {
            string str = "abcdefg";
            Console.WriteLine("string : {0}, Is Unique {1}", str, IsUniqueCharacters(str));
            str = "abcdafg";
            Console.WriteLine("string : {0}, Is Unique {1}", str, IsUniqueCharacters(str));
        }

        private static bool IsUniqueCharacters(string str)
        {
            bool[] chars = new bool[256];

            for (int i = 0; i < str.Length; i++)
            {
                if (chars[str[i]]) return false;

                chars[str[i]] = true;
            }

            return true;
        }
    }
}
