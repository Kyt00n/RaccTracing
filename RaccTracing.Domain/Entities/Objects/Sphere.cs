using RaccTracing.Domain.Entities.Hittable;
using RaccTracing.Domain.Materials;

namespace RaccTracing.Domain.Entities.Objects;

public class Sphere : Hittable.Hittable
{
    private Ray Center { get; }
    private double Radius { get; }
    private Material Material { get; set; }
    
    public Sphere(Point3 staticCenter, double radius, Material material)
    {
        Center = new Ray(staticCenter, new Vec3(0, 0, 0));
        Radius = radius;
        Material = material;
    }
    
    public Sphere(Point3 centerStart, Point3 centerEnd, double radius, Material material)
    {
        Center = new Ray(centerStart, centerEnd - centerStart);
        Radius = radius;
        Material = material;
    }

    public override bool Hit(Ray r, double tMin, double tMax, ref HitRecord rec)
    {
        var currentCenter = Center.At(r.Time);
        var oc = currentCenter - r.Origin;
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
        rec.P = r.At(rec.T);
        rec.Normal = (rec.P - currentCenter) / Radius;
        rec.Material = Material;
        var outwardNormal = (rec.P - currentCenter) / Radius;
        rec.SetFaceNormal(r, outwardNormal);
        return true;
    }

    public override bool Hit(Ray r, Interval rayT, ref HitRecord rec)
    {
        var currentCenter = Center.At(r.Time);
        var oc = currentCenter - r.Origin;
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
        rec.P = r.At(rec.T);
        rec.Normal = (rec.P - currentCenter) / Radius;
        rec.Material = Material;
        var outwardNormal = (rec.P - currentCenter) / Radius;
        rec.SetFaceNormal(r, outwardNormal);
        return true;
    }
}