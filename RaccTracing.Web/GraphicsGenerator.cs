using System.Text;
using Microsoft.Extensions.Configuration;
using RaccTracing.Application.Interfaces;
using RaccTracing.Domain.Entities;

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
        //TODO: Refactor this to use a model and a mapper
        var renderImageSetup = _configuration.GetSection("RenderImageSetup");
        var imageWidth = renderImageSetup.GetValue<int>("ImageWidth");
        var aspectRatio = renderImageSetup.GetValue<double>("AspectRatio");
        var focalLength = renderImageSetup.GetValue<double>("FocalLength");
        var viewportHeight = renderImageSetup.GetValue<double>("ViewPortHeight");
        var cameraCenterSection = renderImageSetup.GetSection("CameraCenter");
        var cameraCenter = new Point3(
            cameraCenterSection.GetValue<double>("X"),
            cameraCenterSection.GetValue<double>("Y"),
            cameraCenterSection.GetValue<double>("Z")
        );

        var cameraSettings = new CameraSettings
        {
            ImageWidth = imageWidth,
            AspectRatio = aspectRatio,
            FocalLength = focalLength,
            ViewportHeight = viewportHeight,
            CameraCenter = cameraCenter
        };
        //TODO: Move to a service
        StringBuilder sb = new();
        sb.Append($"P3\n{cameraSettings.ImageWidth} {cameraSettings.ImageHeight}\n255\n");

        for (var j = 0; j < cameraSettings.ImageHeight; j++)
        {
            Console.WriteLine($"Scan lines remaining: {cameraSettings.ImageHeight - j}");
            for (var i = 0; i < cameraSettings.ImageWidth; i++)
            {
                var pixelColor = new Vec3((double)i / (imageWidth - 1), (double)j / (cameraSettings.ImageHeight - 1), 0.0);
                _colorService.WriteColor(sb, pixelColor);
            }
        }

        Console.WriteLine("Done");
        File.WriteAllText(filePath, sb.ToString());
        Console.WriteLine($"Output saved to {filePath}");
    }
}