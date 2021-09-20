using System.Linq;

namespace Calculator
{
    public class InvalidSeparatorChecker : IInputChecker
    {
        private readonly string _input;
        private readonly INumberParser _numberParser;
        private readonly ISeparatorProvider _separatorProvider;

        public InvalidSeparatorChecker(string input, INumberParser numberParser, ISeparatorProvider separatorProvider)
        {
            _input = input;
            _numberParser = numberParser;
            _separatorProvider = separatorProvider;
        }

        public string Check(string[] numbers)
        {
            for (var numberIndex = 0; numberIndex < numbers.Length; numberIndex++)
            {
                var stringNumber = numbers[numberIndex];
                if (string.IsNullOrEmpty(stringNumber))
                    continue;
                var isParsed = _numberParser.TryParse(stringNumber, out var number);
                if (isParsed)
                    continue;
                var invalidChar = stringNumber.FirstOrDefault(c => !char.IsNumber(c));

                var refInput = _input;
                var separators = _separatorProvider.GetSeparators(ref refInput);

                var errorPosition = refInput.IndexOf(invalidChar);
                return $"'{separators[0]}' expected but '{invalidChar}' found at position {errorPosition}";
            }
            return null;
        }
    }
}