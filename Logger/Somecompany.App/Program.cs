using Somecompany.Core;
using Somecompany.Datalayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Somecompany.App
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceContainer.Instance.Register(typeof(ILogger),typeof(ConsoleLogger));
            DataAccessor accessor = new DataAccessor();
            accessor.Select();
            accessor.Select();
            accessor.Insert("aaa");
            accessor.Insert("hhh");
            Console.ReadKey();
        }
    }
}
