using System;

namespace StringCalculator
{
    public class SumCalculator : ISumCalculator
    {
        public SumCalculator(INumberParser numberParser)
        {
            _numberParser = numberParser;
        }
        private readonly System.Globalization.CultureInfo _culture = new System.Globalization.CultureInfo("en-US");
        private readonly INumberParser _numberParser;

        public string Sum(string[] numbers)
        {
            var result = 0d;

            for (var splitIndex = 0; splitIndex < numbers.Length; splitIndex++)
            {
                var stringNumber = numbers[splitIndex];
                var isParsed = _numberParser.TryParse(stringNumber, out var number);
                if (isParsed)
                {
                    result += number;
                }
            }
            return CorrectFloatingNumbers(result).ToString(_culture);
        }
        private static double CorrectFloatingNumbers(double result, int decimals = 1)
        {
            return Math.Round(result, decimals);
        }

    }


}