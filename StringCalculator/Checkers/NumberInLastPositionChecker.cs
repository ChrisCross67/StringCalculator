using System.Linq;

namespace StringCalculator
{
    public class NumberInLastPositionChecker : IInputChecker
    {
        public string Check(string[] numbers)
        {
            var lastNumber = numbers.LastOrDefault();
            if (string.IsNullOrEmpty(lastNumber))
                return "Number expected but EOF found";

            return null;
        }
    }
}