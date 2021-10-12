using System.Reflection;

namespace DNI.MigrationManager.Modules
{
    public static class This
    {
        public static Assembly Assembly => typeof(This).Assembly;
    }
}
