namespace RaccTracing.Domain.Entities;

public abstract class Hittable
{
    public abstract bool Hit(Ray r, double tMin, double tMax, ref HitRecord rec);
}