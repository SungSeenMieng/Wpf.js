using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfjs.HostClass
{
    public class DebuggerHost
    {
        public void dump(object obj)
        {
            Console.WriteLine(obj);
        }
    }
}
