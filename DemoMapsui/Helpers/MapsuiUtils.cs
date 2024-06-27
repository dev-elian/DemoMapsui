using Mapsui;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Nts.Extensions;
using Mapsui.Projections;
using Mapsui.Styles;
using Mapsui.Widgets;
using Mapsui.Widgets.Zoom;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoMapsui.Helpers;

public static class MapsuiUtils
{
    public static LineString CreateLineString(IEnumerable<Coordinate> coordinates)
    {
        return new LineString(coordinates.Select(v => SphericalMercator.FromLonLat(v.X, v.Y).ToCoordinate()).ToArray());
    }

    public static MemoryLayer CreateLinestringLayer(LineString lineString, string layerName = "Line String", ICollection<IStyle> styles = null)
    {
        return new MemoryLayer
        {
            Features = [
                new GeometryFeature {
                    Geometry = lineString,
                    Styles = styles
                }],
            Name = layerName
        };
    }

    public static IStyle GetRoundedLineStyle(double width, Color color, PenStyle penStyle = PenStyle.Solid, float opacity = 1, double minVisible = 0, double maxVisible = double.MaxValue)
    {
        return new VectorStyle
        {
            Line = new Pen
            {
                Color = color,
                PenStrokeCap = PenStrokeCap.Round,
                StrokeJoin = StrokeJoin.Round,
                PenStyle = penStyle,
                Width = width
            },
            MinVisible = minVisible,
            MaxVisible = maxVisible,
            Opacity = opacity
        };
    }
    public static IStyle GetSquaredLineStyle(double width, Color color, PenStyle penStyle = PenStyle.Solid, float opacity = 1, double minVisible = 0, double maxVisible = double.MaxValue)
    {
        return new VectorStyle
        {
            Line = new Pen
            {
                Color = color,
                PenStrokeCap = PenStrokeCap.Square,
                StrokeJoin = StrokeJoin.Miter,
                PenStyle = penStyle,
                Width = width
            },
            MinVisible = minVisible,
            MaxVisible = maxVisible,
            Opacity = opacity
        };
    }

    public static Color HexToMapsuiColor(string hexColor)
    {
        if (hexColor == null || hexColor.Length != 7 || !hexColor.StartsWith("#"))
            return null;

        try
        {
            byte r = Convert.ToByte(hexColor.Substring(1, 2), 16);
            byte g = Convert.ToByte(hexColor.Substring(3, 2), 16);
            byte b = Convert.ToByte(hexColor.Substring(5, 2), 16);
            return new Color(r, g, b);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static ZoomInOutWidget CreateZoomInOutWidget(Orientation orientation, 
                                                        VerticalAlignment verticalAlignment, 
                                                        HorizontalAlignment horizontalAlignment, 
                                                        float marginX = 20,
                                                        float marginY = 20)
    {
        return new ZoomInOutWidget
        {
            Orientation = orientation,
            VerticalAlignment = verticalAlignment,
            HorizontalAlignment = horizontalAlignment,
            MarginX = marginX,
            MarginY = marginY
        };
    }
    public static MRect GetMRect(double lon, double lat, double ratioInMeters)
    {
        var coordinate = SphericalMercator.FromLonLat(lon, lat).ToCoordinate();
        return new MRect(coordinate.X - ratioInMeters, coordinate.Y - ratioInMeters, coordinate.X + ratioInMeters, coordinate.Y + ratioInMeters);
    }

}
