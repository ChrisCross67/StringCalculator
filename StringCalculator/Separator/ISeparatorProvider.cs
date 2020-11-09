namespace StringCalculator
{
    public interface ISeparatorProvider
    {
        string[] GetSeparators(ref string input);
    }
}