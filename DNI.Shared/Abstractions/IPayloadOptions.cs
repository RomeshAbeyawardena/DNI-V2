namespace DNI.Shared.Abstractions
{
    public interface IPayloadOptions
    {
        char HeaderSeparator { get; }
        char ParameterSeparator { get; }
    }
}
