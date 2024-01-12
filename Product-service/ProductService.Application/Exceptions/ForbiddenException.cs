using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Application.Exceptions
{
    public class ForbiddenException(string message): Exception(message)
    {
        
    }
}