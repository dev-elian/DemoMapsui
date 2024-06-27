using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoMapsui.Helpers;
using Mapsui;
using Mapsui.Animations;
using Mapsui.Widgets.Zoom;
using System.Collections.ObjectModel;

namespace DemoMapsui.ViewModels;

public partial class ZoomMapsuiViewModel : BaseMapViewModel
{
    public ZoomMapsuiViewModel(){
        Map.Info += OnChangeMapInfo;
        LoadZoomExample();
    }
    void LoadZoomExample()
    {
        Map.Layers.Clear();
        Map.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());
        Map.Widgets.Enqueue(new ZoomInOutWidget
        {
            Orientation = Orientation.Horizontal,
            VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Bottom,
            HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Left,
            MarginX = 20,
            MarginY = 20,
        });
    }
    void OnChangeMapInfo(object? sender, MapInfoEventArgs a)
    {
        if (a.MapInfo?.WorldPosition != null)
        {
            switch (SelectedItem)
            {
                case "Center And Zoom":
                    Map.Navigator.CenterOnAndZoomTo(a.MapInfo.WorldPosition, a.MapInfo.Resolution * 0.5, 500, Easing.CubicOut);
                    break;
                case "Zoom To":
                    Map.Navigator.ZoomTo(a.MapInfo.Resolution * 0.5, a.MapInfo.ScreenPosition!, 500, Easing.CubicOut);
                    break;
                default:
                    break;
            }
        }
    }

    #region Properties
    [ObservableProperty] string _selectedItem = "Center And Zoom";
    public ObservableCollection<string> Items { get; } = [
        "Center And Zoom",
        "Zoom To",
        "Slider"
        ];
    partial void OnSelectedItemChanged(string value)
    {
        SliderVisible = value == "Slider";
    }

    [ObservableProperty] bool _sliderVisible;
    [ObservableProperty] double _sliderValue = 1000;
    partial void OnSliderValueChanged(double value)
    {
        if (SelectedItem == "Slider")
            Map.Navigator.ZoomTo(value);
    }

    [ObservableProperty] bool _zoomLocked = false;
    partial void OnZoomLockedChanged(bool value)
    {
        Map.Navigator.ZoomLock = value;
    }
    #endregion

    #region Commands

    [RelayCommand]
    void ZoomIn()
    {
        Map.Navigator.ZoomIn(500, Easing.Linear);
    }

    [RelayCommand]
    void ZoomOut()
    {
        Map.Navigator.ZoomOut(500, Easing.CubicOut);
    }

    [RelayCommand]
    void ZoomToLevel()
    {
        Map.Navigator.ZoomToLevel(13);
    }

    [RelayCommand]
    void ZoomToBox()
    {
        Map.Navigator.ZoomToBox(MapsuiUtils.GetMRect(-64.734334, -21.533918, 300));
    }

    [RelayCommand]
    void ZoomToPanBounds()
    {
        Map.Navigator.ZoomToPanBounds();
    }

    #endregion
}
