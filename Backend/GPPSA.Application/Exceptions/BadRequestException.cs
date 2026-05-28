using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPPSA.Application.Exceptions
{
    public class BadRequestException:Exception
    {
        public BadRequestException(string message): base(message)
        {
        }
    }
}