using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PrintNumbersAsString
{
    class Program
    {
        static void Main(string[] args)
        {
            string number = "1823421459";

            string words = ConvertToWords(number);

            string result = "one billion eight hundred twenty three million four hundred twenty one thousand four hundred fifty nine";

            Console.WriteLine("are the same ? {0}", string.CompareOrdinal(result, words) == 0);

            Console.WriteLine(result);
            Console.WriteLine(words);

            Console.Read();
        }

        private static string ConvertToWords(string number)
        {
           return new NumberToWordsConverter(number).Convert();
        }
    }
}
