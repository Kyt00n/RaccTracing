namespace RaccTracing.Domain.Entities;

public class Ray(Point3 origin, Vec3 direction)
{
    private Point3 Origin { get; } = origin;

    private Vec3 Direction { get; } = direction;

    public Vec3 At(double t)
    {
        return Origin + Direction * t;
    }
}
