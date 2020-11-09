namespace StringCalculator
{
    public class StringCalculator
    {
        private readonly INumberExtractor _numberExtractor;
        private readonly ISumCalculator _sumCalculator;
        private readonly ICalculatorChecker _checker;

        public StringCalculator(INumberExtractor numberExtractor, ISumCalculator sumCalculator, ICalculatorChecker checker = null)
        {
            _numberExtractor = numberExtractor;
            _sumCalculator = sumCalculator;
            _checker = checker;
        }

        public string Add(string input)
        {
            var numbers = _numberExtractor.Exctract(input);

            var errorMessage = _checker?.Check(numbers);
            if (!string.IsNullOrEmpty(errorMessage))
                return errorMessage;

            var result = _sumCalculator.Sum(numbers);

            return result;
        }
    }
}