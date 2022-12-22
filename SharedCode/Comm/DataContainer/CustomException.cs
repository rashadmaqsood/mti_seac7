using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedCode.Comm.DataContainer
{
    public class CustomException
    {
        public bool isTrue;
        public Exception Ex;
        public long SomeNumber = 0;
        public string SomeMessage = "Nothing";

        public CustomException()
        {
            isTrue = true;
            Ex = null;
        }
    }
}
