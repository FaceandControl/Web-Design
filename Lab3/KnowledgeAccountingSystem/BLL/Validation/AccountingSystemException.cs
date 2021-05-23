using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BLL.Validation
{
    [Serializable]
    public class AccountingSystemException : Exception
    {
        public AccountingSystemException()
        {
        }

        public AccountingSystemException(string message) : base(message)
        {
        }

        public AccountingSystemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountingSystemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
