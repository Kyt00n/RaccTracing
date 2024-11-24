namespace RaccTracing.Domain.Entities.Hittable;

public abstract class Hittable
{
    //TODO: Remove Hit method from Hittable class withouth the interval
    public abstract bool Hit(Ray r, double tMin, double tMax, ref HitRecord rec);
    public abstract bool Hit(Ray r, Interval rayT, ref HitRecord rec);
    public abstract AxisAlignedBoundingBox BoundingBox();
}