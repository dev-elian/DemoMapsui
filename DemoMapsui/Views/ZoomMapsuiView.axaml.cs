using Avalonia.Controls;
using Avalonia.Interactivity;
using DemoMapsui.ViewModels;

namespace DemoMapsui.Views;

public partial class ZoomMapsuiView : UserControl
{
    ZoomMapsuiViewModel vm = new();
    public ZoomMapsuiView()
    {
        InitializeComponent();
        DataContext = vm;
        mapControl.Map = ((ZoomMapsuiViewModel)DataContext).Map;
    }
}