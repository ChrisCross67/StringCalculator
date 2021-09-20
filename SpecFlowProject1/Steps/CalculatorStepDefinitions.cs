using FluentAssertions;
using Calculator;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.Steps
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        private string _result;

        private StringCalculator _stringCalculator;
        private string _input;

        public CalculatorStepDefinitions(ScenarioContext scenarioContext)
        {

        }

        [Given(@"the input is ""(.*)""")]
        public void GivenTheInputIs(string input)
        {
            _input = input;

            var separators = new string[] { ",", "\n" };
            var numberParser = new NumberParser();
            var separatorProvider = new SeparatorProvider(separators);
            var inputChecker = new CalculatorChecker(
                           new EmptyInputChecker(),
                           new NumberInLastPositionChecker(),
                           new NegativeNumberChecker(numberParser),
                           new InvalidNumberChecker(input),
                           new InvalidSeparatorChecker(input, numberParser, separatorProvider));
            var numberExtractor = new NumberExtractor(separatorProvider);
            var sumCalculator = new SumCalculator(numberParser);
            _stringCalculator = new StringCalculator(numberExtractor, sumCalculator, inputChecker);
        }


        [When(@"I calculate the sum")]
        public void WhenICalculateTheSum()
        {
            _result = _stringCalculator.Add(_input);
        }

        [Then(@"the result should be ""(.*)""")]
        public void ThenTheResultShouldBe(string result)
        {
            _result.Should().Be(result);
        }
        [Then(@"the result should contains ""(.*)""")]
        public void ThenTheResultShouldContains(string result)
        {
            _result.Should().Contain(result);
        }
    }
}
