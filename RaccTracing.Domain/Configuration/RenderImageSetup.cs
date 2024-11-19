namespace RaccTracing.Domain.Configuration;

public class RenderImageSetup
{
    public int ImageWidth { get; set; }
    public double AspectRatio { get; set; }
    public double FocalLength { get; set; }
    public int SamplesPerPixel { get; set; }
    public int MaxDepth { get; set; }
    public double VerticalFov { get; set; }
    public double DefocusAngle { get; set; }
    public double FocusDistance { get; set; }
    public LookFromSetup LookFrom { get; set; }
    public LookAtSetup LookAt { get; set; }
    public VUpSetup Up { get; set; }
    public bool IsMultiThreaded { get; set; }
}