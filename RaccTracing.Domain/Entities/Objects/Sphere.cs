namespace RaccTracing.Domain.Entities.Objects;

public class Sphere(Point3 center, double radius) : Hittable
{
    private Point3 Center { get; } = center;
    private double Radius { get; } = radius;

    public override bool Hit(Ray r, double tMin, double tMax, ref HitRecord rec)
    {
        var oc = Center - r.Origin;
        var a = r.Direction.LengthSquared();
        var h = Vec3.Dot(r.Direction, oc);
        var c = oc.LengthSquared() - Radius * Radius;
        var delta = h * h - a * c;
        if (delta < 0)
        {
            return false;
        }
        var squareRootOfDelta = Math.Sqrt(delta);
        
        var root = (h-squareRootOfDelta) / a;
        if (root <= tMin || root >= tMax)
        {
            root = (h + squareRootOfDelta) / a;
            if (root <= tMin || root >= tMax)
            {
                return false;
            }
        }
        rec.T = root;
        rec.P = new Point3(r.At(rec.T).X, r.At(rec.T).Y, r.At(rec.T).Z);
        rec.Normal = (rec.P - Center) / Radius;
        
        return true;
    }
}