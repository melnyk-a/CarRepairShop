using System;

namespace CarRepairShop.Data
{
    public class DataException : Exception
    {
        public DataException(string message) : base(message)
        {
        }
    }
}