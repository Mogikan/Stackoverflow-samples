using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Somecompany.Core
{
    public interface ILogger
    {
        void Log(string tag,string message);
    }
}
