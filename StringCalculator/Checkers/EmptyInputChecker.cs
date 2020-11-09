namespace StringCalculator
{
    public class EmptyInputChecker : IInputChecker
    {
        private const string DEFAULT_RESULT = "0";
        public string Check(string[] numbers)
        {
            if (numbers.Length < 2)
                return DEFAULT_RESULT;
            return null;
        }
    }


}