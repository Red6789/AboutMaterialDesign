using System.ComponentModel.DataAnnotations;
using System;

namespace ErrorTemplateBinding.Validates;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class ExcludeSpaceAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
            return false;

        if (value is not string stringValue) 
            return false;

        return stringValue.IndexOf(" ", StringComparison.Ordinal) < 0;
    }
}