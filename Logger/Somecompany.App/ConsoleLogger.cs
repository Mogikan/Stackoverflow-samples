using Somecompany.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Somecompany.App
{
    class ConsoleLogger : ILogger
    {
        public void Log(string tag, string message)
        {
            Console.WriteLine(string.Format("{0}:{1}",tag,message));
        }
    }
}
