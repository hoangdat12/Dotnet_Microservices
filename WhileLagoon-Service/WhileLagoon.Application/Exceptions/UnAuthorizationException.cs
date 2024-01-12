using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhileLagoon.Application.Exceptions
{
    public class UnAuthorizationException(string message): Exception(message)
    {
        
    }
}