namespace StringCalculator
{
    public class SeparatorProvider : ISeparatorProvider
    {
        private readonly string[] _separators;
        private readonly string[] DEFAULT_SEPARATOR = new string[] { "," };
        public SeparatorProvider(string[] separators = null)
        {
            _separators = separators ?? DEFAULT_SEPARATOR;
        }
        public string[] GetSeparators(ref string input)
        {
            return _separators;
        }
    }
}