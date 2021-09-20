namespace Calculator
{
    public interface INumberParser
    {
        double Parse(string input);
        bool TryParse(string input,out double result);
    }
}