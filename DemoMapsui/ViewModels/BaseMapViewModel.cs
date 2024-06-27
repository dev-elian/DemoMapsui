using CommunityToolkit.Mvvm.ComponentModel;
using Mapsui;

namespace DemoMapsui.ViewModels;

public partial class BaseMapViewModel:ViewModelBase
{
    [ObservableProperty]
    Map _map = new();
}
