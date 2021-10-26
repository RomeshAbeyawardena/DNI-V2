namespace DNI.Tests.Shared.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmail(this string value)
        {
            return value.Contains("@") && value.Contains(".");
        }
    }
}
