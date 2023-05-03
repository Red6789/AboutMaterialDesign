using System.Threading.Tasks;
using ErrorTemplateBinding.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace ErrorTemplateBinding.ViewModels;

public class MainViewModel : BindableBase
{
    public MainViewModel()
    {
        ShowCommand = new DelegateCommand(OnShowEvent);
    }

    private async Task SetModelUserName(string append)
    {
        OneTimeModel.UserName += append;
        OneWayModel.UserName += append;
        await Task.Delay(1000);
    }

    private async void OnShowEvent()
    {
        IsEnabled = false;

        OneTimeModel.UserName = string.Empty;
        OneWayModel.UserName = string.Empty;
        await Task.Delay(1000);
        await SetModelUserName("1");
        await SetModelUserName(" ");
        await SetModelUserName("2");

        IsEnabled = true;
    }

    private bool _isEnabled = true;
    public bool IsEnabled
    {
        set => SetProperty(ref _isEnabled, value);
        get => _isEnabled;
    }

    public DelegateCommand ShowCommand { get; private init; }

    public UserModel OneTimeModel { get; } = new();

    public UserModel OneWayModel { get; } = new();
}