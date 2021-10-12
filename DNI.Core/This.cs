using System.Reflection;

namespace DNI.Core
{
    public static class This
    {
        public static Assembly Assembly => typeof(This).Assembly;
    }
}
