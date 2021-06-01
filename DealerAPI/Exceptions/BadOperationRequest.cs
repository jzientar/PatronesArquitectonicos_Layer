﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerAPI.Exceptions
{
    public class BadOperationRequest : Exception
    {
        public BadOperationRequest(string message)
            : base(message)
        {

        }
    }
}
