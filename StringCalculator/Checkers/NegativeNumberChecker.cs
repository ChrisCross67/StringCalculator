using System.Text;

namespace Calculator
{
    public class NegativeNumberChecker : IInputChecker
    {
        private readonly INumberParser _numberParser;

        public NegativeNumberChecker(INumberParser numberParser)
        {
            _numberParser = numberParser;
        }
        public string Check(string[] numbers)
        {
            var negativeNumbers = new StringBuilder();
            foreach (var number in numbers)
            {
                if (string.IsNullOrEmpty(number))
                    continue;
                if (_numberParser.TryParse(number, out var result) && result < 0)
                    negativeNumbers.Append($" {number},");
            }
            return negativeNumbers.Length > 1
                ? $"Negative not allowed :{negativeNumbers.ToString(0, negativeNumbers.Length - 1)}"
                : null;
        }
    }
}