using DemoMapsui.Helpers;
using Mapsui.Styles;
using Mapsui;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;

namespace DemoMapsui.ViewModels;

public partial class LinesMapsuiViewModel:BaseMapViewModel
{
    public LinesMapsuiViewModel()
    {
        LoadLines();
    }

    [RelayCommand]
    void LoadLines()
    {
        Map.Layers.Clear();
        List<Coordinate> _coordinates = [
            new Coordinate(-67.750997, -20.227364),
            new Coordinate(-64.526198, -18.952416),
            new Coordinate(-64.526198, -20.946708),
            new Coordinate(-61.740773, -20.970922)
            ];

        Map.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());

        Color color = MapsuiUtils.HexToMapsuiColor("#FF0856");

        ICollection<IStyle> styles = [
            MapsuiUtils.GetRoundedLineStyle(14, Color.Black),
            MapsuiUtils.GetSquaredLineStyle(9, color, PenStyle.ShortDash),
        ];

        LineString lineString = MapsuiUtils.CreateLineString(_coordinates);
        Map.Layers.Add(MapsuiUtils.CreateLinestringLayer(lineString, "Line Layer", styles));
    }
}
