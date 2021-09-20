using System;

namespace Calculator
{
    public class NumberExtractor : INumberExtractor
    {
        private readonly ISeparatorProvider _separatorProvider;
        public NumberExtractor(ISeparatorProvider separatorProvider)
        {
            _separatorProvider = separatorProvider;
        }

        public string[] Exctract(string input)
        {
            var separators = _separatorProvider?.GetSeparators(ref input);
            var stringNumbers = input.Split(separators, StringSplitOptions.None);
            if (stringNumbers.Length == 1 && string.IsNullOrEmpty(stringNumbers[0]))
                return new string[]{ "0" };
            return stringNumbers;
        }
    }
}