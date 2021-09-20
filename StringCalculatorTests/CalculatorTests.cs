using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod("An empty string will return 0")]
        public void AnEmptyStringWillReturn0()
        {
            var expected = "0";
            var input = "";
            var numberParser = new NumberParser();
            var inputChecker = new CalculatorChecker();
            var separatorProvider = new SeparatorProvider();
            var numberExtractor = new NumberExtractor(separatorProvider);
            var sumCalculator = new SumCalculator(numberParser);
            var calculator = new StringCalculator(numberExtractor, sumCalculator, inputChecker);
            var result = calculator.Add(input);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod()]
        [DataRow("1", "1", DisplayName = "1 should return 1")]
        [DataRow("1.1", "1.1", DisplayName = "1.1 should return 1.1")]
        [DataRow("2.2", "2.2", DisplayName = "2.2 should return 2.2")]
        public void SingleInputShouldReturnTheNumber(string input, string expected)
        {
            var separatorProvider = new SeparatorProvider();
            var numberExtractor = new NumberExtractor(separatorProvider);
            var numberParser = new NumberParser();
            var sumCalculator = new SumCalculator(numberParser);
            var calculator = new StringCalculator(numberExtractor, sumCalculator);
            var result = calculator.Add(input);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod()]
        [DataRow("1.1,2.2", "3.3", DisplayName = "1.1,2.2 should return 3.3")]
        [DataRow("1,2,3,4,5", "15", DisplayName = "if input is from 1 to 5 should return 15")]
        public void MultipleNumbersSouldReturnTheirSum(string input, string expected)
        {
            var separatorProvider = new SeparatorProvider();
            var numberExtractor = new NumberExtractor(separatorProvider);
            var numberParser = new NumberParser();
            var sumCalculator = new SumCalculator(numberParser);
            var calculator = new StringCalculator(numberExtractor, sumCalculator);
            var result = calculator.Add(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod("Allow the add method to handle newlines as separators")]
        public void AddShouldReturn6IfInputContainsNewLineSeparator()
        {
            var input = "1\n2,3";
            var expected = "6";
            var separators = new string[] { ",", "\n" };
            var inputChecker = new CalculatorChecker(new EmptyInputChecker());
            var separatorProvider = new SeparatorProvider(separators);
            var numberExtractor = new NumberExtractor(separatorProvider);
            var numberParser = new NumberParser();
            var sumCalculator = new SumCalculator(numberParser);
            var calculator = new StringCalculator(numberExtractor, sumCalculator, inputChecker);
            var result = calculator.Add(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod("Add should return the message 'Number expected but \n found at position 6. if input is 175.2,\n35")]
        public void AddShouldReturnNumberExpectedWhenInvalidMessage()
        {
            var input = "175.2,\n35";
            var expected = "Number expected but '\n' found at position 6";
            var separators = new string[] { ",", "\n" };
            var numberParser = new NumberParser();
            var inputChecker = new CalculatorChecker(new InvalidNumberChecker(input));
            var separatorProvider = new SeparatorProvider(separators);
            var numberExtractor = new NumberExtractor(separatorProvider);
            var sumCalculator = new SumCalculator(numberParser);
            var calculator = new StringCalculator(numberExtractor, sumCalculator, inputChecker);
            var result = calculator.Add(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod("Add should return 'Number expected but EOF found' when the input ends with a separator")]
        public void AddShouldReturnAnErrorMessageWhenInputEndsWithASeparator()
        {
            var input = "1,3,";
            var expected = "Number expected but EOF found";
            var inputChecker = new CalculatorChecker(new NumberInLastPositionChecker());
            var separatorProvider = new SeparatorProvider();
            var numberExtractor = new NumberExtractor(separatorProvider);
            var numberParser = new NumberParser();
            var sumCalculator = new SumCalculator(numberParser);
            var calculator = new StringCalculator(numberExtractor, sumCalculator, inputChecker);
            var result = calculator.Add(input);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod()]
        [DataRow("//;\n1;2","3",DisplayName = "'//;\n1;2' should return 3")]
        [DataRow("//|\n1|2|3", "6",DisplayName = "'//|\n1|2|3' should return 6")]
        [DataRow("//sep\n2sep3", "5",DisplayName = "'//sep\n2sep3' should return 5")]
        [DataRow("//|\n1|2,3", "'|' expected but ',' found at position 3", DisplayName = "'//|\n1|2,3' is invalid and should return the message '|' expected but ',' found at position 3.")]
        public void CustomSeparatorsTest(string input, string expected)
        {
            var separatorProvider = new CustomSeparatorProvider(input);
            var numberParser = new NumberParser();
            var inputChecker = new CalculatorChecker(
                new EmptyInputChecker(), 
                new NumberInLastPositionChecker(), 
                new InvalidNumberChecker(input),
                new InvalidSeparatorChecker(input, numberParser, separatorProvider));

            var numberExtractor = new NumberExtractor(separatorProvider);
            var sumCalculator = new SumCalculator(numberParser);
            var calculator = new StringCalculator(numberExtractor, sumCalculator, inputChecker);
            var result = calculator.Add(input);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod()]
        [DataRow("-1,2", "Negative not allowed : -1",DisplayName = "-1,2 is invalid and should return the message Negative not allowed : -1")]
        [DataRow("2,-4,-5", "Negative not allowed : -4, -5", DisplayName = "2,-4,-5 is invalid and should return the message Negative not allowed : -4, -5")]
        public void NegativeNumbersTest(string input, string expected)
        {
            var separatorProvider = new CustomSeparatorProvider(input);
            var numberParser = new NumberParser();
            var inputChecker = new CalculatorChecker(
                new EmptyInputChecker(),
                new NumberInLastPositionChecker(),
                new InvalidNumberChecker(input),
                new InvalidSeparatorChecker(input, numberParser, separatorProvider),
                new NegativeNumberChecker(numberParser));

            var numberExtractor = new NumberExtractor(separatorProvider);
            var sumCalculator = new SumCalculator(numberParser);
            var calculator = new StringCalculator(numberExtractor, sumCalculator, inputChecker);
            var result = calculator.Add(input);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod()]
        [DataRow("-1,,2", "Negative not allowed : -1\nNumber expected but ',' found at position 3")]
        public void MultipleErrorsTest(string input, string expected)
        {
            var separatorProvider = new CustomSeparatorProvider(input);
            var numberParser = new NumberParser();
            var inputChecker = new CalculatorChecker(
                new EmptyInputChecker(),
                new NumberInLastPositionChecker(),
                new NegativeNumberChecker(numberParser),
                new InvalidNumberChecker(input),
                new InvalidSeparatorChecker(input, numberParser, separatorProvider));

            var numberExtractor = new NumberExtractor(separatorProvider);
            var sumCalculator = new SumCalculator(numberParser);
            var calculator = new StringCalculator(numberExtractor, sumCalculator, inputChecker);
            var result = calculator.Add(input);
            Assert.AreEqual(expected, result);

        }
    }
}