using CarRepairShop.Wpf.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace CarRepairShop.Wpf.ViewModels
{
    public abstract class ValidatableViewModel : ViewModel, INotifyDataErrorInfo
    {
        private readonly IDictionary<string, ICollection<string>> propertyToErrors = new Dictionary<string, ICollection<string>>();
        private ICollection<PropertyInfo> validationProperties = new List<PropertyInfo>();

        public bool HasErrors => propertyToErrors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected void AddError(string propertyName, string error)
        {
            if (!propertyToErrors.TryGetValue(propertyName, out ICollection<string> errors))
            {
                errors = new List<string>();

                propertyToErrors.Add(propertyName, errors);
            }

            errors.Add(error);

            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
        }

        protected void ClearError(string propertyName)
        {
            if (propertyToErrors.Remove(propertyName))
            {
                OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
            }
        }

        protected abstract IEnumerable<string> GetErrors(string propertyName, string propertyValue);

        private void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }

        protected override void HandlePropertiesAttributes(PropertyInfo appliedProperty)
        {
            base.HandlePropertiesAttributes(appliedProperty);

            var attribute = appliedProperty.GetCustomAttribute<ValidatablePropertyAttribute>();
            if (attribute != null)
            {
                validationProperties.Add(appliedProperty);
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            //Validate(e.PropertyName);

            base.OnPropertyChanged(e);
        }

        protected void Validate()
        {
            foreach (var property in validationProperties)
            {
                Validate(property.Name, (string)property.GetValue(this));
            }
        }

    protected void Validate(string propertyName, string propertyValue)
    {
        ClearError(propertyName);

        IEnumerable<string> errors = GetErrors(propertyName, propertyValue);

        foreach (var error in errors)
        {
            AddError(propertyName, error);
        }
    }

    IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
    {
        IEnumerable<string> result;

        if (string.IsNullOrEmpty(propertyName))
        {
            var allErrors = new List<string>();

            foreach (ICollection<string> errors in propertyToErrors.Values)
            {
                allErrors.AddRange(errors);
            }

            result = allErrors;
        }
        else
        {
            if (propertyToErrors.ContainsKey(propertyName))
            {
                result = propertyToErrors[propertyName];
            }
            else
            {
                result = new string[0];
            }
        }

        return result;
    }
}
}