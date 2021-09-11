namespace DNI.Shared
{
    public class PlaceholderInfo
    {
        public PlaceholderInfo(char startCharacter, char endCharacter, string value, int position)
        {
            StartCharacter = startCharacter;
            EndCharacter = endCharacter;
            Value = value;
            Position = position;
        }
        public char StartCharacter { get; }
        public char EndCharacter { get; }
        public string Value { get; }
        public int Position { get; }
        public int? Length => Value?.Length;
    }
}
