namespace Calculator
{
    public class NumberParser : INumberParser
    {
        private readonly System.Globalization.CultureInfo _culture = new System.Globalization.CultureInfo("en-US");
        private const double DEFAULT_VALUE = 0d;
        public double Parse(string input)
        {
            return double.Parse(input, System.Globalization.NumberStyles.Float, _culture);
        }

        public bool TryParse(string input, out double result)
        {
            return double.TryParse(input, System.Globalization.NumberStyles.Float, _culture, out result);
        }
    }
}