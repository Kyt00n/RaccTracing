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
        var sphereCenter = new Point3(0, 0, -1);
        var t = IsSphereHit(sphereCenter, 0.5, r);
        if (t > 0.0)
        {
            var normal = (r.At(t) - new Vec3(0, 0, -1)).UnitVector();
            var colorOfPixel = 0.5 * new Color(normal.X + 1, normal.Y + 1, normal.Z + 1);
            return new Color(colorOfPixel.X, colorOfPixel.Y, colorOfPixel.Z);
        }
        var unitDirection = r.Direction.UnitVector();
        var a = 0.5 * (unitDirection.Y + 1.0);
        var blendedValue = (1.0-a) * Colors.White + a * Colors.LightBlue;
        return new Color(blendedValue.X, blendedValue.Y, blendedValue.Z);
    }

    private static double IsSphereHit(Point3 center, double radius, Ray r)
    {
        var oc = center - r.Origin;
        var a = r.Direction.LengthSquared();
        var h = Vec3.Dot(r.Direction, oc);
        var c = oc.LengthSquared() - radius * radius;
        var discriminant = h * h - a * c;
        if (discriminant < 0)
        {
            return -1.0;
        }
        return (h - Math.Sqrt(discriminant)) / a;
    }
    
    private static int Round(double value)
    {
        return (int)(255.999 * value);
    }
}