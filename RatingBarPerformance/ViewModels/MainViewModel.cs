using Prism.Mvvm;
using RatingBarPerformance.Models;
using System.Collections.ObjectModel;

namespace RatingBarPerformance.ViewModels;

public class MainViewModel : BindableBase
{
    public MainViewModel()
    {
        for (int i = 1; i <= 3; i++)
        {
            var tabItem = new TestTabItemModel($"Header{i}");
            for (int j = 0; j < 100; j++)
            {
                tabItem.ItemModels.Add(new TestItemModel());
            }
            TabModels1.Add(tabItem);
        }

        for (int i = 1; i <= 3; i++)
        {
            var tabItem = new TestTabItemModel($"Header{i}");
            for (int j = 0; j < 100; j++)
            {
                tabItem.ItemModels.Add(new TestItemModel());
            }
            TabModels2.Add(tabItem);
        }
    }

    public ObservableCollection<TestTabItemModel> TabModels1 { get; } = new();

    public ObservableCollection<TestTabItemModel> TabModels2 { get; } = new();
}