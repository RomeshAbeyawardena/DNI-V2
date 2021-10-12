using System.Reflection;

namespace DNI.Mediator.Core
{
    public static class This
    {
        public static Assembly Assembly => typeof(This).Assembly;
    }
}
