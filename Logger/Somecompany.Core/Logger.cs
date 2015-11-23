using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Somecompany.Core
{
    public static class Logger
    {
        public static void Log(string tag, string message)
        {
            var logger = ServiceContainer.Instance.Resolve<ILogger>();
            logger.Log(tag, message);
        }
    }
}
