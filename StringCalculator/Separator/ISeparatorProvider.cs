namespace Calculator
{
    public interface ISeparatorProvider
    {
        string[] GetSeparators(ref string input);
    }
}