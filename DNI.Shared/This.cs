using System;
using System.Reflection;

namespace DNI.Shared
{
    public static class This
    {
        public static Assembly Assembly => Assembly.GetAssembly(typeof(This));
    }
}
