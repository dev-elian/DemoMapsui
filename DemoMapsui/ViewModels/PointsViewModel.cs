using CommunityToolkit.Mvvm.Input;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Styles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DemoMapsui.ViewModels;

public partial class PointsViewModel:BaseMapViewModel
{
    MemoryLayer _pointsLayer = new()
    {
        Name = "Points",
        Style = CreateBitmapStyle(),
        Features = new List<IFeature>()
    };

    public PointsViewModel()
    {
        Map.Layers.Clear();
        Map.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());
        Map.Layers.Add(_pointsLayer);
        Map.Info += OnChangeMapInfo;
    }

    static SymbolStyle CreateBitmapStyle()
    {
        var bitmapId = GetBitmapIdForEmbeddedResource(@"DemoMapsui.Assets.gps.png");
        var bitmapHeight = 200; 
        return new SymbolStyle { 
            BitmapId = bitmapId, 
            SymbolScale = 0.2, 
            SymbolOffset = new Offset(0, bitmapHeight * 0.5) 
        };
    }

    void OnChangeMapInfo(object? sender, MapInfoEventArgs e)
    {
        if (e.MapInfo?.WorldPosition != null)
        {
            var feature = new PointFeature(
                new MPoint(e.MapInfo.WorldPosition.X, e.MapInfo.WorldPosition.Y)
                );
            ((List<IFeature>)_pointsLayer.Features).Add(feature);
            Map.RefreshGraphics();
        }
    }

    [RelayCommand]
    void AddSinglePoint()
    {
        var random = new Random();
        var lng = random.NextDouble() * 360 - 180; // Random longitude between -180 y 180
        var lat = random.NextDouble() * 180 - 90;  // Random latutide between -90 y 90

        var feature = new PointFeature(
            SphericalMercator.FromLonLat(lng, lat).ToMPoint()
            );

        ((List<IFeature>)_pointsLayer.Features).Add(feature);
        Map.RefreshGraphics();
    }

    [RelayCommand]
    void AddMultiplePoints()
    {
        List<IFeature> features = [];

        for (int i = 0; i < 10; i++)
        {
            var random = new Random();
            var lng = random.NextDouble() * 360 - 180; // Random longitude between -180 y 180
            var lat = random.NextDouble() * 180 - 90;  // Random latutide between -90 y 90

            var feature = new PointFeature(
                SphericalMercator.FromLonLat(lng, lat).ToMPoint()
                );
            features.Add(feature);
        }

        ((List<IFeature>)_pointsLayer.Features).AddRange(features);
        Map.RefreshGraphics();
    }

    [RelayCommand]
    void CleanMap()
    {
        ((List<IFeature>)_pointsLayer.Features).Clear();
        Map.RefreshGraphics();
        //Map.Layers.Remove(x => x.Name == "Points");
    }

    public static int GetBitmapIdForEmbeddedResource(string resourceName)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        using Stream stream = assembly.GetManifestResourceStream(resourceName);
        if (stream != null)
        {
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            var bitmapId = BitmapRegistry.Instance.Register(memoryStream);

            return bitmapId;
        }

        return -1;
    }
}