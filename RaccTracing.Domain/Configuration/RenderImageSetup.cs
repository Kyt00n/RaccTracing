namespace RaccTracing.Domain.Configuration;

public class RenderImageSetup
{
    public int ImageWidth { get; set; }
    public double AspectRatio { get; set; }
    public double FocalLength { get; set; }
    public double ViewPortHeight { get; set; }
    public int SamplesPerPixel { get; set; }
    public CameraCenterSetup CameraCenter { get; set; }
}