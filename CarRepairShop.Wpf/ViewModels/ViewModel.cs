﻿using CarRepairShop.Wpf.Attributes;
using CarRepairShop.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CarRepairShop.Wpf.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        private readonly Type currentType;
        protected readonly PropertyInfo[] properties;

        public ViewModel()
        {
            currentType = GetType();
            properties = currentType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                HandlePropertiesAttributes(property);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void HandleDependsUpponProperties(PropertyInfo appliedProperty)
        {
            IEnumerable<DependsUponPropertyAttribute> attributes = appliedProperty.GetCustomAttributes<DependsUponPropertyAttribute>();
            foreach (DependsUponPropertyAttribute attribute in attributes)
            {
                PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == attribute.PropertyName)
                    {
                        OnPropertyChanged(new PropertyChangedEventArgs(appliedProperty.Name));
                    }
                };
            }
        }

        protected virtual void HandlePropertiesAttributes(PropertyInfo appliedProperty)
        {
            HandleDependsUpponProperties(appliedProperty);
            HandleRaiseCanExecuteDependsUpponProperties(appliedProperty);
        }

        private void HandleRaiseCanExecuteDependsUpponProperties(PropertyInfo appliedProperty)
        {
            RaiseCanExecuteDependsUponAttribute attribute = appliedProperty.GetCustomAttribute<RaiseCanExecuteDependsUponAttribute>();
            if (attribute != null)
            {
                PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == attribute.PropertyName)
                    {
                        Command command = (Command)appliedProperty.GetValue(this);
                        command.RaiseCanExecuteChanged();
                    }
                };
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected virtual void SetProperty<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!oldValue?.Equals(newValue) ?? newValue != null)
            {
                oldValue = newValue;

                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}