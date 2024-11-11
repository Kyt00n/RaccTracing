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
            SamplesPerPixel = renderImageSetup.SamplesPerPixel,
            VerticalFov = renderImageSetup.VerticalFov,
            MaxDepth = renderImageSetup.MaxDepth,
            LookFrom = new Point3(
                renderImageSetup.LookFrom.X,
                renderImageSetup.LookFrom.Y,
                renderImageSetup.LookFrom.Z
            ),
            LookAt = new Point3(
                renderImageSetup.LookAt.X,
                renderImageSetup.LookAt.Y,
                renderImageSetup.LookAt.Z
            ),
            VUp = new Vec3(
                renderImageSetup.Up.X,
                renderImageSetup.Up.Y,
                renderImageSetup.Up.Z
            )
        };
    }
}