using System.Reflection;

namespace DNI.Web.Modules
{
    public static class This
    {
        public static Assembly Assembly => typeof(This).Assembly;
    }
}
