using Avalonia.Controls;
using Avalonia.Interactivity;
using DemoMapsui.ViewModels;
using System.Diagnostics;

namespace DemoMapsui.Views;

public partial class LinesMapsuiView : UserControl
{
    LinesMapsuiViewModel vm = new();
    public LinesMapsuiView()
    {
        InitializeComponent();
        DataContext = vm;
        mapControl.Map = ((LinesMapsuiViewModel)DataContext).Map;
    }
}