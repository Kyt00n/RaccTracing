namespace RaccTracing.Domain.Entities;

public class CameraSettings(
    int imageWidth,
    double aspectRatio,
    double focalLength,
    double viewportHeight,
    int samplesPerPixel,
    int maxDepth,
    Point3 cameraCenter)
{
    public CameraSettings() : this(0, 0.0, 0.0, 0.0, 1,1,new Point3(0, 0, 0))
    {
    }

    public int ImageWidth { get; init; } = imageWidth;
    public double AspectRatio { get; init; } = aspectRatio;
    public double FocalLength { get; init; } = focalLength;
    public double ViewportHeight { get; init; } = viewportHeight;
    public int SamplesPerPixel { get; init; } = samplesPerPixel;
    public int MaxDepth { get; init; } = maxDepth;
    public Point3 CameraCenter { get; init; } = cameraCenter;
    

    public int ImageHeight => CalculateImageHeight();
    public double ViewportWidth  => ViewportHeight * ((double)ImageWidth/ImageHeight);
    public Vec3 ViewportU => new(ViewportWidth, 0, 0);
    public Vec3 ViewportV => new(0, -ViewportHeight, 0);
    public Vec3 PixelDeltaU => ViewportU / ImageWidth;
    public Vec3 PixelDeltaV => ViewportV / ImageHeight;
    
    public Vec3 ViewportUpperLeft => CameraCenter - new Vec3(0,0,FocalLength) - ViewportU/2 - ViewportV/2;
    
    public Vec3 Pixel00Location => ViewportUpperLeft + 0.5*(PixelDeltaU + PixelDeltaV);
    
    public double PixelSamplesScale => 1.0 / SamplesPerPixel;
    
    private int CalculateImageHeight()
    {
        var imageHeight= (int)(ImageWidth / AspectRatio);
        return imageHeight < 1 ? 1 : imageHeight;
    }
}