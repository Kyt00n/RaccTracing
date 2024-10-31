using System.Text;
using RaccTracing.Application.Interfaces;
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
    
    private int Round(double value)
    {
        return (int)(255.999 * value);
    }
}