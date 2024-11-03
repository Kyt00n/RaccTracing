namespace RaccTracing.Domain.Entities;

public class HitRecord
{
    public Point3 P { get; set; }
    public Vec3 Normal { get; set; }
    public double T { get; set; }
}