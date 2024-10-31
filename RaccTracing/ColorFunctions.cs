using System.Text;

namespace RaccTracing;

public static class ColorFunctions
{
    public static void WriteColor(StringBuilder output, Color pixelColor)
    {
        var r = pixelColor.X();
        var g = pixelColor.Y();
        var b = pixelColor.Z();

        var rbyte = (int)(255.999 * r);
        var gbyte = (int)(255.999 * g);
        var bbyte = (int)(255.999 * b);

        output.AppendLine($"{rbyte} {gbyte} {bbyte}");
    }
}