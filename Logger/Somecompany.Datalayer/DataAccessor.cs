using Somecompany.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Somecompany.Datalayer
{
    public class DataAccessor
    {
        public DataAccessor()
        {

        }

        ILogger logger;
        public string Select()
        {
            Logger.Log("datalayer","select");
            return "data";
        }

        public void Insert(string data)
        {
            Logger.Log("datalayer",string.Format("{0} inserted",data));
        }
    }
}
