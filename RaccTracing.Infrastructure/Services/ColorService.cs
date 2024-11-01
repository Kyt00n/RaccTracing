using System.Text;
using RaccTracing.Application.Interfaces;
using RaccTracing.Domain.ColorConstants;
using RaccTracing.Domain.Entities;

namespace RaccTracing.Infrastructure.Services;

public class ColorService : IColorService
{
    public void WriteColor(StringBuilder output, Vec3 pixelColor)
    {
        
        var r = pixelColor.X;
        var g = pixelColor.Y;
        var b = pixelColor.Z;

        var rByte = Round(r);
        var gByte = Round(g);
        var bByte = Round(b);

        output.AppendLine($"{rByte} {gByte} {bByte}");
    }

    public Color RayColor(Ray r)
    {
        if (IsSphereHit(new Point3(0, 0, -1), 0.5, r))
        {
            return Colors.Red;
        }
        var unitDirection = r.Direction.UnitVector();
        var a = 0.5 * (unitDirection.Y + 1.0);
        var blendedValue = (1.0-a) * Colors.White + a * Colors.LightBlue;
        return new Color(blendedValue.X, blendedValue.Y, blendedValue.Z);
    }

    private static bool IsSphereHit(Point3 center, double radius, Ray r)
    {
        var oc = center - r.Origin;
        var a = Vec3.Dot(r.Direction, r.Direction);
        var b = 2.0 * Vec3.Dot(oc, r.Direction);
        var c = Vec3.Dot(oc, oc) - radius * radius;
        var discriminant = b * b - 4 * a * c;
        return discriminant >= 0;
    }
    
    private static int Round(double value)
    {
        return (int)(255.999 * value);
    }
}