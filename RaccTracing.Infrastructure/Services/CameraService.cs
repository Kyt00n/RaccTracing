using System.Text;
using RaccTracing.Application.Interfaces;
using RaccTracing.Domain.ColorConstants;
using RaccTracing.Domain.Constants;
using RaccTracing.Domain.Entities;
using RaccTracing.Domain.Entities.Hittable;

namespace RaccTracing.Infrastructure.Services;

public class CameraService : ICameraService
{
    public void Render(StringBuilder output, Hittable world, CameraSettings cameraSettings)
    {
        output.Append($"P3\n{cameraSettings.ImageWidth} {cameraSettings.ImageHeight}\n255\n");
        
        for (var j = 0; j < cameraSettings.ImageHeight; j++)
        {
            Console.WriteLine($"Scan lines remaining: {cameraSettings.ImageHeight - j}");
            for (var i = 0; i < cameraSettings.ImageWidth; i++)
            {
                var pixelCenter = cameraSettings.Pixel00Location + 
                                  i * cameraSettings.PixelDeltaU + 
                                  j * cameraSettings.PixelDeltaV;
                var rayDirection = pixelCenter - cameraSettings.CameraCenter;
                var ray = new Ray(cameraSettings.CameraCenter, rayDirection);

                var pixelColor = RayColor(ray, world);
                WriteColor(output, pixelColor);
            }
        }
        Console.WriteLine("Done");
    }

    private void WriteColor(StringBuilder output, Vec3 pixelColor)
    {
        
        var r = pixelColor.X;
        var g = pixelColor.Y;
        var b = pixelColor.Z;

        var rByte = Round(r);
        var gByte = Round(g);
        var bByte = Round(b);

        output.AppendLine($"{rByte} {gByte} {bByte}");
    }

    private Color RayColor(Ray r, Hittable world)
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