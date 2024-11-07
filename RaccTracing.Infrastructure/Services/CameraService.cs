using System.Text;
using RaccTracing.Application.Interfaces;
using RaccTracing.Domain.ColorConstants;
using RaccTracing.Domain.Constants;
using RaccTracing.Domain.Entities;
using RaccTracing.Domain.Entities.Hittable;

namespace RaccTracing.Infrastructure.Services;

public class CameraService : ICameraService
{
    //TODO: move camera settings to constructor
    //TODO: fix converting vec3 color and point3
    public void Render(StringBuilder output, Hittable world, CameraSettings cameraSettings)
    {
        output.Append($"P3\n{cameraSettings.ImageWidth} {cameraSettings.ImageHeight}\n255\n");
        
        for (var j = 0; j < cameraSettings.ImageHeight; j++)
        {
            Console.WriteLine($"Scan lines remaining: {cameraSettings.ImageHeight - j}");
            for (var i = 0; i < cameraSettings.ImageWidth; i++)
            {
                var pixelColor = new Color(0, 0, 0);
                for (int sample = 0; sample < cameraSettings.SamplesPerPixel; sample++)
                {
                    var ray = GetRay(i, j, cameraSettings);
                    var rayColor = RayColor(ray, world);
                    pixelColor += RayColor(ray, world);
                }
            }
        }
        Console.WriteLine("Done");
    }

    private void WriteColor(StringBuilder output, Vec3 pixelColor)
    {
        
        var r = pixelColor.X;
        var g = pixelColor.Y;
        var b = pixelColor.Z;
        
        var intensity = new Interval(0.000, 0.999);
        
        var rByte = Round(intensity.Clamp(r));
        var gByte = Round(intensity.Clamp(g));
        var bByte = Round(intensity.Clamp(b));

        output.AppendLine($"{rByte} {gByte} {bByte}");
    }

    private Ray GetRay(int i, int j, CameraSettings cameraSettings)
    {
        var offset = SampleSquare();
        var pixelSample = cameraSettings.Pixel00Location
            + ((i + offset.X) * cameraSettings.PixelDeltaU)
            + ((j + offset.Y) * cameraSettings.PixelDeltaV);
        var rayOrigin = cameraSettings.CameraCenter;
        var rayDirection = pixelSample - rayOrigin;
        
        return new Ray(rayOrigin, rayDirection);
    }
    private Vec3 SampleSquare()
    {
        // Returns the vector to a random point in the [-.5,-.5]-[+.5,+.5] unit square.
        return new Vec3(Constants.RandomDouble()-0.5, Constants.RandomDouble()-0.5, 0);
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
        return (int)(256 * value);
    }
}