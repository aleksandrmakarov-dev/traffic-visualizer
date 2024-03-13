﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Exceptions
{
    public class UnprocessableEntityException : Exception
    {
        public UnprocessableEntityException(string? message) : base(message) { }
    }
}
