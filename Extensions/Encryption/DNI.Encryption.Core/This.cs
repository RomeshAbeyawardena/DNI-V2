using System.Reflection;

namespace DNI.Encryption.Core
{
    public static class This
    {
        public static Assembly Assembly => typeof(This).Assembly;
    }
}
