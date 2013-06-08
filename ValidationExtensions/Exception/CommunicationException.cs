using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidationExtensions.Exception
{
    public class CommunicationException : System.Exception
    {
        private System.Exception exc;

        public CommunicationException(System.Exception exc)
        {
            this.exc = exc;
        }
    }
}
