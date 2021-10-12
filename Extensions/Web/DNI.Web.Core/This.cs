using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Web.Core
{
    public static class This
    {
        public static Assembly Assembly => typeof(This).Assembly;
    }
}
