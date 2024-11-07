using Microsoft.Extensions.Configuration;
using RaccTracing.Domain.Configuration;
using RaccTracing.Domain.Entities;

namespace RaccTracing.Web;

public class CameraSettingsInitializer
{
    public CameraSettings CameraSettings { get; }

    public CameraSettingsInitializer(IConfiguration configuration)
    {
        var renderImageSetup = configuration.GetSection("RenderImageSetup").Get<RenderImageSetup>();
        if (renderImageSetup == null)
        {
            throw new Exception("RenderImageSetup is not configured");
        }
        CameraSettings = new CameraSettings
        {
            ImageWidth = renderImageSetup.ImageWidth,
            AspectRatio = renderImageSetup.AspectRatio,
            FocalLength = renderImageSetup.FocalLength,
            ViewportHeight = renderImageSetup.ViewPortHeight,
            SamplesPerPixel = renderImageSetup.SamplesPerPixel,
            MaxDepth = renderImageSetup.MaxDepth,
            CameraCenter = new Point3(
                renderImageSetup.CameraCenter.X,
                renderImageSetup.CameraCenter.Y,
                renderImageSetup.CameraCenter.Z
            ),
        };
    }
}