using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class NotAllowedException : Exception
    {
        public NotAllowedException()
        : base()
        {
        }

        public NotAllowedException(string message)
            : base(message)
        {
        }

        public NotAllowedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
