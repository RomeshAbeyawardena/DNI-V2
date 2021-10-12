using System.Reflection;

namespace DNI.Mediator.Modules
{
    public static class This
    {
        public static Assembly Assembly => typeof(This).Assembly;
    }
}
