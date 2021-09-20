namespace Calculator
{
    public class EmptyInputChecker : IInputChecker
    {
        private const string DEFAULT_RESULT = "0";
        public string Check(string[] numbers)
        {
            if (numbers.Length < 1 )
                return DEFAULT_RESULT;
            if(numbers.Length == 1 && string.IsNullOrEmpty(numbers[0]))
                return DEFAULT_RESULT;
            return null;
        }
    }


}