namespace RaccTracing.Domain.Entities;

public class Ray(Point3 origin, Vec3 direction, double time = 0.0)
{
    public Point3 Origin { get; } = origin;

    public Vec3 Direction { get; } = direction;
    
    public double Time { get; } = time;

    public Vec3 At(double t)
    {
        return Origin + Direction * t;
    }
}
