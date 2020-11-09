namespace StringCalculator
{
    public class CustomSeparatorProvider : ISeparatorProvider
    {
        private readonly string _input;
        private readonly string _triggerValue;
        private readonly string _endOfSeparator;

        public CustomSeparatorProvider(string input,string triggerValue = "//",string endOfSeparator = "\n")
        {
            _input = input;
            _triggerValue = triggerValue;
            _endOfSeparator = endOfSeparator;
        }
        public string[] GetSeparators(ref string input)
        {
            if(_input.StartsWith(_triggerValue))
            {
                var separator = _input.Substring(_triggerValue.Length).Split(_endOfSeparator)[0];
                input = input.Substring(_triggerValue.Length + separator.Length+ _endOfSeparator.Length);
                return new string[] { separator };
            }
            return new SeparatorProvider().GetSeparators(ref input);
        }
    }
}