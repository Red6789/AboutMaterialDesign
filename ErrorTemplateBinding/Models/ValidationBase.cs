using Prism.Mvvm;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Collections;
using System.Linq;

namespace ErrorTemplateBinding.Models;

public abstract class ValidationBase : BindableBase, INotifyDataErrorInfo
{
    private readonly ConcurrentDictionary<string, List<string>> _errors = new();

    protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null!)
    {
        var result = base.SetProperty(ref storage, value, propertyName);
        if (result)
        {
            Validate(value, propertyName);
        }
        return result;
    }

    protected void AddError(string propertyName, string errorMessage)
    {
        var errorList = _errors.GetOrAdd(propertyName, k => new List<string>());
        if (!errorList.Contains(errorMessage))
        {
            errorList.Add(errorMessage);
            OnErrorsChanged(propertyName);
        }
    }

    protected void RemoveError(string propertyName, string errorMessage)
    {
        if (_errors.TryGetValue(propertyName, out var errorList))
        {
            errorList.Remove(errorMessage);
            OnErrorsChanged(propertyName);
        }
    }

    protected void ClearErrors()
    {
        _errors.Clear();
    }

    protected void AddOrRemoveError(Func<bool> predicate, string errorMessage, string anotherPropertyName, [CallerMemberName]string propertyName = null!)
    {
        if (predicate())
        {
            RemoveError(propertyName, errorMessage);
            RemoveError(anotherPropertyName, errorMessage);
        }
        else
        {
            AddError(propertyName, errorMessage);
        }
    }

    private void Validate<T>(T val, string propertyName)
    {
        _errors.TryRemove(propertyName, out _);

        var context = new ValidationContext(this) { MemberName = propertyName };
        List<ValidationResult> results = new();

        if (!Validator.TryValidateProperty(val, context, results))
        {
            _errors[propertyName] = results.Select(x => x.ErrorMessage).ToList()!;
        }

        OnErrorsChanged(propertyName);
    }

    public bool HasErrors => _errors.Any(kv => kv.Value is { Count: > 0 });

    private void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        if (_errors.TryGetValue(propertyName!, out var errorsForName))
        {
            return new List<string> { string.Join("\r\n", errorsForName) };
        }
        return errorsForName!;
    }
}