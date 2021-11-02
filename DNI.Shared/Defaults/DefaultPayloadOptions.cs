using DNI.Shared.Abstractions;

namespace DNI.Shared.Defaults
{
    public class DefaultPayloadOptions : IPayloadOptions
    {
        public static IPayloadOptions Default => new DefaultPayloadOptions
        {
            HeaderSeparator = ':',
            ParameterSeparator = '|'
        };

        public char HeaderSeparator { get; init; }
        public char ParameterSeparator { get; init; }
    }
}
