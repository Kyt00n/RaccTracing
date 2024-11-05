using System.Text;
using RaccTracing.Application.Interfaces;
using RaccTracing.Domain.ColorConstants;
using RaccTracing.Domain.Constants;
using RaccTracing.Domain.Entities;
using RaccTracing.Domain.Entities.Hittable;

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

    public Color RayColor(Ray r, Hittable world)
    {
        HitRecord rec = new();
        if (world.Hit(r, new Interval(0, Constants.Infinity), ref rec))
        {
            var colorVec3 = 0.5 * (rec.Normal + new Color(1,1,1));
            return new Color(colorVec3.X, colorVec3.Y, colorVec3.Z);
        }
        var unitDirection = r.Direction.UnitVector();
        var a = 0.5 * (unitDirection.Y + 1.0);
        var blendedValue = (1.0-a) * Colors.White + a * Colors.LightBlue;
        return new Color(blendedValue.X, blendedValue.Y, blendedValue.Z);
    }
    
    private static int Round(double value)
    {
        return (int)(255.999 * value);
    }
}