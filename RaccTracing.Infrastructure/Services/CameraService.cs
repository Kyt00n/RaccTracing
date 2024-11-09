using System.Text;
using RaccTracing.Application.Interfaces;
using RaccTracing.Domain.ColorConstants;
using RaccTracing.Domain.Constants;
using RaccTracing.Domain.Entities;
using RaccTracing.Domain.Entities.Hittable;

namespace RaccTracing.Infrastructure.Services;

public class CameraService : ICameraService
{
    private readonly CameraSettings _cameraSettings;

    public CameraService(CameraSettings cameraSettings)
    {
        _cameraSettings = cameraSettings;
    }

    public void Render(StringBuilder output, Hittable world)
    {
        output.Append($"P3\n{_cameraSettings.ImageWidth} {_cameraSettings.ImageHeight}\n255\n");
        
        for (var j = 0; j < _cameraSettings.ImageHeight; j++)
        {
            Console.WriteLine($"Scan lines remaining: {_cameraSettings.ImageHeight - j}");
            for (var i = 0; i < _cameraSettings.ImageWidth; i++)
            {
                var pixelColor = new Color(0, 0, 0);
                for (int sample = 0; sample < _cameraSettings.SamplesPerPixel; sample++)
                {
                    var ray = GetRay(i, j);
                    pixelColor += RayColor(ray,_cameraSettings.MaxDepth ,world);
                }
                WriteColor(output, pixelColor*_cameraSettings.PixelSamplesScale);
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

    private Ray GetRay(int i, int j)
    {
        var offset = SampleSquare();
        var pixelSample = _cameraSettings.Pixel00Location
            + ((i + offset.X) * _cameraSettings.PixelDeltaU)
            + ((j + offset.Y) * _cameraSettings.PixelDeltaV);
        var rayOrigin = _cameraSettings.CameraCenter;
        var rayDirection = pixelSample - rayOrigin;
        
        return new Ray(rayOrigin, rayDirection);
    }
    private Vec3 SampleSquare()
    {
        // Returns the vector to a random point in the [-.5,-.5]-[+.5,+.5] unit square.
        return new Vec3(Constants.RandomDouble()-0.5, Constants.RandomDouble()-0.5, 0);
    }
    private Color RayColor(Ray r,int depth ,Hittable world)
    {
        if (depth <= 0)
        {
            return Colors.Black;
        }
        HitRecord rec = new();
        if (world.Hit(r, new Interval(0.001, Constants.Infinity), ref rec))
        {
            var direction = rec.Normal + Vec3.RandomUnitVector();
            return 0.5 * RayColor(new Ray(rec.P, direction),depth-1, world);
        }
        var unitDirection = r.Direction.UnitVector();
        var a = 0.5 * (unitDirection.Y + 1.0);
        return (1.0-a) * Colors.White + a * Colors.LightBlue;
    }
    
    private static int Round(double value)
    {
        return (int)(256 * value);
    }
}