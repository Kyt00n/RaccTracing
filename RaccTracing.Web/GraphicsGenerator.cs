using System.Text;
using Microsoft.Extensions.Configuration;
using RaccTracing.Application.Interfaces;
using RaccTracing.Domain.Configuration;
using RaccTracing.Domain.Entities;
using RaccTracing.Domain.Entities.Hittable;
using RaccTracing.Domain.Entities.Objects;

namespace RaccTracing.Web;

public class GraphicsGenerator
{
    private readonly IColorService _colorService;
    private readonly IConfiguration _configuration;

    public GraphicsGenerator(IColorService colorService, IConfiguration configuration)
    {
        _colorService = colorService;
        _configuration = configuration;
    }

    public void GenerateImage(string filePath)
    {
        var renderImageSetup = _configuration.GetSection(nameof(RenderImageSetup)).Get<RenderImageSetup>();
        if (renderImageSetup == null)
        {
            throw new Exception("RenderImageSetup is not configured");
        }
        
        var cameraSettings = new CameraSettings
        {
            ImageWidth = renderImageSetup.ImageWidth,
            AspectRatio = renderImageSetup.AspectRatio,
            FocalLength = renderImageSetup.FocalLength,
            ViewportHeight = renderImageSetup.ViewPortHeight,
            CameraCenter = new Point3(
                renderImageSetup.CameraCenter.X,
                renderImageSetup.CameraCenter.Y,
                renderImageSetup.CameraCenter.Z
            ),
        };
        
        //TODO: Move to a service
        StringBuilder sb = new();
        sb.Append($"P3\n{cameraSettings.ImageWidth} {cameraSettings.ImageHeight}\n255\n");
        HittableList world = new();
        world.Add(new Sphere(new Point3(0, 0, -1), 0.5));
        world.Add(new Sphere(new Point3(0, -100.5, -1), 100));
        
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

                var pixelColor = _colorService.RayColor(ray, world);
                _colorService.WriteColor(sb, pixelColor);
            }
        }

        Console.WriteLine("Done");
        File.WriteAllText(filePath, sb.ToString());
        Console.WriteLine($"Output saved to {filePath}");
    }
}