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
    Vec3 vUp)
{
    public CameraSettings() : this(0, 0.0, 1,1, 90,0.0,0.0,new Point3(0, 0, 0), new Point3(0, 0, -1), new Vec3(0, 1, 0))
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
    
    
    public Vec3 W => (LookFrom - LookAt).UnitVector();
    public Vec3 U => Vec3.Cross(VUp,W).UnitVector();
    public Vec3 V => Vec3.Cross(W,U);
    public double FocalLength => (LookFrom - LookAt).Length();

    public int ImageHeight => CalculateImageHeight();
    public double ViewportWidth  => ViewportHeight * ((double)ImageWidth/ImageHeight);
    public double ViewportHeight => 2.0 * h * FocusDistance;
    public Vec3 ViewportU => ViewportWidth * U;
    public Vec3 ViewportV => ViewportHeight * -V;
    public Vec3 PixelDeltaU => ViewportU / ImageWidth;
    public Vec3 PixelDeltaV => ViewportV / ImageHeight;
    
    public Vec3 ViewportUpperLeft => LookFrom - (FocusDistance *W) - ViewportU/2 - ViewportV/2;
    
    public Vec3 Pixel00Location => ViewportUpperLeft + 0.5*(PixelDeltaU + PixelDeltaV);
    
    public double PixelSamplesScale => 1.0 / SamplesPerPixel;
    
    public Vec3 DefocusDiskU => U * DefocusRadius;
    public Vec3 DefocusDiskV => V * DefocusRadius;
    
    public double Theta => Constants.Constants.DegreesToRadians(VerticalFov);
    private double h => Math.Tan(Theta / 2);
    private double DefocusRadius => FocusDistance * Math.Tan(Constants.Constants.DegreesToRadians(DefocusAngle) / 2);
    
    private int CalculateImageHeight()
    {
        var imageHeight= (int)(ImageWidth / AspectRatio);
        return imageHeight < 1 ? 1 : imageHeight;
    }
}