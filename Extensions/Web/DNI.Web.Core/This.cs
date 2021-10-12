using System.Reflection;

namespace DNI.Web.Core
{
    public static class This
    {
        public static Assembly Assembly => typeof(This).Assembly;
    }
}
