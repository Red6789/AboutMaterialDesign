using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace RatingBarPerformance.Models;

public class TestTabItemModel : BindableBase
{
    public TestTabItemModel(string title)
    {
        Title = title;
    }

    public string Title { get; private init; }

    private bool _isSelected;
    public bool IsSelected
    {
        set => SetProperty(ref _isSelected, value);
        get => _isSelected;
    }

    public ObservableCollection<TestItemModel> ItemModels { get; } = new();
}