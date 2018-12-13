using System;

namespace CarRepairShop.Wpf.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ValidatablePropertyAttribute : Attribute
    {
    }
}