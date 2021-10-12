using System.Reflection;

namespace DNI.Data.Core
{
    public static class This
    {
        public static Assembly Assembly => typeof(This).Assembly;
    }
}
