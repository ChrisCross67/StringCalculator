using System;

namespace StringCalculator
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
            return input.Split(separators, StringSplitOptions.None);
        }
    }
}