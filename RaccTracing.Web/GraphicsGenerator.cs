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
    private readonly ICameraService _cameraService;
    private readonly IConfiguration _configuration;

    public GraphicsGenerator(ICameraService cameraService, IConfiguration configuration)
    {
        _cameraService = cameraService;
        _configuration = configuration;
    }

    public CameraSettings InitializeCameraSettings()
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
        return cameraSettings;
    }
    public void GenerateImage(string filePath)
    {
        var cameraSettings = InitializeCameraSettings();
        StringBuilder sb = new();
        
        HittableList world = new();
        world.Add(new Sphere(new Point3(0, 0, -1), 0.5));
        world.Add(new Sphere(new Point3(0, -100.5, -1), 100));
        
        _cameraService.Render(sb, world, cameraSettings);
        
        File.WriteAllText(filePath, sb.ToString());
        Console.WriteLine($"Output saved to {filePath}");
    }
}