﻿using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Finance.Core
{
    public abstract class ViewModelBase : ObservableObject, INotifyDataErrorInfo
    {
        Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

        public bool HasErrors => Errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (Errors.ContainsKey(propertyName))
            {
                return Errors[propertyName];
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }

        public void Validate(string propertyName, object propertyValue, RelayCommand propertyCommand)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);

            if (results.Any())
            {
                Errors.Add(propertyName, results.Select(item => item.ErrorMessage).ToList());
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                Errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }

            if (propertyCommand != null)
                propertyCommand.RaiseCanExecuteChanged();
        }
    }
}
