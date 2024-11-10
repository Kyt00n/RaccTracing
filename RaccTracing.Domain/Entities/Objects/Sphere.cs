using RaccTracing.Domain.Entities.Hittable;
using RaccTracing.Domain.Materials;

namespace RaccTracing.Domain.Entities.Objects;

public class Sphere(Point3 center, double radius, Material material) : Hittable.Hittable
{
    private Point3 Center { get; } = center;
    private double Radius { get; } = radius;
    private Material Material { get; set; } = material;

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
        rec.Material = Material;
        var outwardNormal = (rec.P - Center) / Radius;
        rec.SetFaceNormal(r, outwardNormal);
        return true;
    }

    public override bool Hit(Ray r, Interval rayT, ref HitRecord rec)
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
        if (!rayT.Surrounds(root))
        {
            root = (h + squareRootOfDelta) / a;
            if (!rayT.Surrounds(root))
            {
                return false;
            }
        }
        rec.T = root;
        rec.P = new Point3(r.At(rec.T).X, r.At(rec.T).Y, r.At(rec.T).Z);
        rec.Normal = (rec.P - Center) / Radius;
        rec.Material = Material;
        var outwardNormal = (rec.P - Center) / Radius;
        rec.SetFaceNormal(r, outwardNormal);
        return true;
    }
}