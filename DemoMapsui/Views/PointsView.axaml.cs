using Avalonia.Controls;
using DemoMapsui.ViewModels;

namespace DemoMapsui.Views;

public partial class PointsView : UserControl
{
    PointsViewModel vm = new();
    public PointsView()
    {
        InitializeComponent();
        DataContext = vm;
        mapControl.Map = ((PointsViewModel)DataContext).Map;
    }
}