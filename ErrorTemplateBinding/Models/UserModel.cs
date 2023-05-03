using System.ComponentModel.DataAnnotations;
using ErrorTemplateBinding.Validates;

namespace ErrorTemplateBinding.Models;

public class UserModel : ValidationBase
{
    private string _userName = string.Empty;
    [ExcludeSpace(ErrorMessage = "Not contain space")]
    [StringLength(12, MinimumLength = 4, ErrorMessage = "4-12")]
    public string UserName
    {
        set => SetProperty(ref _userName, value);
        get => _userName;
    }
}