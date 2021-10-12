using System.Reflection;

namespace DNI.Data.Modules
{
    public static class This
    {
        public static Assembly Assembly => typeof(This).Assembly;
    }
}
