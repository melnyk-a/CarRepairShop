﻿using System;

namespace CarRepairShop.Domain
{
    public sealed class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}