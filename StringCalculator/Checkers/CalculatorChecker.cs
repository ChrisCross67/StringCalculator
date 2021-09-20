using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class CalculatorChecker : ICalculatorChecker
    {
        private readonly IList<IInputChecker> _checkers;

        public CalculatorChecker(params IInputChecker[] checkers)
        {
            _checkers = checkers;
        }

        public string Check(string[] numbers)
        {
            var errorMessages = new StringBuilder();
            foreach (var checker in _checkers)
            {
                var errorMessage = checker.Check(numbers);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    if (errorMessages.Length > 0)
                        errorMessages.Append("\n");
                    errorMessages.Append(errorMessage);
                }
            }
            return errorMessages.Length > 0 ? errorMessages.ToString() : null;
        }
    }
}