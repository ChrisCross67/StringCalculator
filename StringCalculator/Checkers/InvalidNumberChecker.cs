using System.Linq;

namespace StringCalculator
{
    public class InvalidNumberChecker : IInputChecker
    {
        private readonly string _input;

        public InvalidNumberChecker(string input)
        {
            _input = input;
        }

        public string Check(string[] numbers)
        {
            for (var numberIndex = 0; numberIndex < numbers.Length; numberIndex++)
            {
                var stringNumber = numbers[numberIndex];
                if (!string.IsNullOrEmpty(stringNumber))
                    continue;

                var errorPosition = numbers.Take(numberIndex).Sum(o => o.Length) + 1;
                var errorChar = _input[errorPosition];
                return $"Number expected but '{errorChar}' found at position {errorPosition}";
            }
            return null;
        }
    }

}