using System;
using System.Collections.Generic;

namespace PrintNumbersAsString
{
    internal class NumberToWordsConverter
    {
        private readonly string _number;
        private readonly Dictionary<int, string> _mapIterationToName;
        private readonly Dictionary<string, string> _mapSingleToName;
        private readonly Dictionary<string, string> _mapTensToName;

        public NumberToWordsConverter(string number)
        {
            _number = number;
            _mapIterationToName = new Dictionary<int, string>
            {
                [0] = "billion",
                [1] = "million",
                [2] = "thousand",
                [3] = ""
            };

            _mapSingleToName = new Dictionary<string, string>
            {
                ["0"] = "zero",
                ["1"] = "one",
                ["2"] = "two",
                ["3"] = "three",
                ["4"] = "four",
                ["5"] = "five",
                ["6"] = "six",
                ["7"] = "seven",
                ["8"] = "eight",
                ["9"] = "nine"
            };

            _mapTensToName = new Dictionary<string, string>
            {
                ["10"] = "ten",
                ["11"] = "eleven",
                ["12"] = "twelve",
                ["2"] = "twenty",
                ["3"] = "thirty",
                ["4"] = "forty",
                ["5"] = "fifty",
                ["6"] = "sixty",
                ["7"] = "seventy",
                ["8"] = "eighty",
                ["9"] = "ninety"
            };
        }

        internal string Convert()
        {
            return Convert(_number);
        }

        private string Convert(string number)
        {
            string res = string.Empty;
            
            for (int i = number.Length; i > 0; i = i -3)
            {
                res = res.Insert(0, ConvertSection(SubString(number, i), i / 3));
            }

            return res.TrimEnd();
        }

        private string SubString(string number,int i)
        {
            if ((i - 3) >= 0)
            {
                return number.Substring(i - 3, 3);
            }
            else
            {
                return number.Substring(0, 2 - i);
            }
        }

        private string ConvertSection(string number, int i)
        {
            string result = string.Empty;

            if (number.Length == 3)
            {
                result = ConvertHundred(number);
            }
            else if (number.Length == 2)
            {
                result = ConvertTens(number);
            }
            else
            {
                result = ConvertSingles(number);
            }

            return result + " " + _mapIterationToName[i] + " ";
        }

        private string ConvertHundred(string number)
        {
            string single = number.Substring(0, 1);

            return _mapSingleToName[single] + " hundred " + ConvertTens(number.Substring(1, 2));
        }

        private string ConvertTens(string number)
        {
            string single = number.Substring(0, 1);

            if (single == "1")
            {
                if (_mapTensToName.ContainsKey(number)) return _mapTensToName[number];

                return _mapSingleToName[single] + "teen ";
            }
            else
            {
                return _mapTensToName[single] + " " + ConvertSingles(number.Substring(1, 1));
            }
        }

        private string ConvertSingles(string number)
        {
            return _mapSingleToName[number];
        }
    }
}