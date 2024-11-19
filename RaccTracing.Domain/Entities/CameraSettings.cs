namespace RaccTracing.Domain.Entities;

public class CameraSettings(
    int imageWidth,
    double aspectRatio,
    int samplesPerPixel,
    int maxDepth,
    double verticalFov,
    double defocusAngle,
    double focusDistance,
    Point3 lookFrom,
    Point3 lookAt,
    Vec3 vUp,
    bool isMultiThreaded)
{
    public CameraSettings() : this(0, 0.0, 1,1, 90,0.0,0.0,new Point3(0, 0, 0), new Point3(0, 0, -1), new Vec3(0, 1, 0), false)
    {
    }

    public int ImageWidth { get; init; } = imageWidth;
    public double AspectRatio { get; init; } = aspectRatio;
    
    public int SamplesPerPixel { get; init; } = samplesPerPixel;
    public int MaxDepth { get; init; } = maxDepth;
    public double VerticalFov { get; init; } = verticalFov;
    public double DefocusAngle { get; init; } = defocusAngle;
    public double FocusDistance { get; init; } = focusDistance;
    public Point3 LookFrom { get; init; } = lookFrom;
    public Point3 LookAt { get; init; } = lookAt;
    public Vec3 VUp { get; init; } = vUp;

    public bool IsMultiThreaded { get; init; } = isMultiThreaded;
    
    private Vec3 W =>(LookFrom - LookAt).UnitVector();
    private Vec3 U => Vec3.Cross(VUp,W).UnitVector();
    private Vec3 V => Vec3.Cross(W,U);
    public double FocalLength => (LookFrom - LookAt).Length();

    public int ImageHeight => CalculateImageHeight();
    private double ViewportWidth  => ViewportHeight * ((double)ImageWidth/ImageHeight);
    private double ViewportHeight => 2.0 * h * FocusDistance;
    private Vec3 ViewportU => ViewportWidth * U;

    private Vec3 ViewportV => ViewportHeight * -V;
    private Vec3? _pixelDeltaU;
    public Vec3 PixelDeltaU =>_pixelDeltaU ??= ViewportU / ImageWidth;
    private Vec3? _pixelDeltaV;
    public Vec3 PixelDeltaV =>_pixelDeltaV ??= ViewportV / ImageHeight;

    private Vec3 ViewportUpperLeft => LookFrom - (FocusDistance *W) - ViewportU/2 - ViewportV/2;
    private Vec3? _pixel00Location;
    public Vec3 Pixel00Location =>_pixel00Location ??= ViewportUpperLeft + 0.5*(PixelDeltaU + PixelDeltaV);
    
    public double PixelSamplesScale => 1.0 / SamplesPerPixel;
    private Vec3? _defocusDiskU;
    public Vec3 DefocusDiskU =>_defocusDiskU ??= U * DefocusRadius;
    private Vec3? _defocusDiskV;
    public Vec3 DefocusDiskV =>_defocusDiskV ??= V * DefocusRadius;

    private double Theta => Constants.Constants.DegreesToRadians(VerticalFov);
    private double h => Math.Tan(Theta / 2);
    private double DefocusRadius => FocusDistance * Math.Tan(Constants.Constants.DegreesToRadians(DefocusAngle) / 2);
    
    private int CalculateImageHeight()
    {
        var imageHeight= (int)(ImageWidth / AspectRatio);
        return imageHeight < 1 ? 1 : imageHeight;
    }
}