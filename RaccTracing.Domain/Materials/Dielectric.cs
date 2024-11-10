using RaccTracing.Domain.Entities;
using RaccTracing.Domain.Entities.Hittable;

namespace RaccTracing.Domain.Materials;

public class Dielectric(double refIdx) : Material
{
    private double RefIdx { get; set; } = refIdx;

    public override bool Scatter(Ray rayIn, HitRecord hitRecord, out Vec3 attenuation, out Ray scattered)
    {
        attenuation = new Color(1, 1, 1);
        var ri = hitRecord.FrontFace ? 1.0 / RefIdx : RefIdx;
        
        var unitDirection = rayIn.Direction.UnitVector();
        var cosTheta = Math.Min(Vec3.Dot(-unitDirection, hitRecord.Normal), 1.0);
        var sinTheta = Math.Sqrt(1.0 - cosTheta * cosTheta);
        
        var cannotRefract = ri * sinTheta > 1.0;

        var direction = cannotRefract ||  Schlick(cosTheta, ri) > Constants.Constants.RandomDouble()?
            Vec3.Reflect(unitDirection, hitRecord.Normal) 
            : 
            Vec3.Refract(unitDirection, hitRecord.Normal, ri);
        
        scattered = new Ray(hitRecord.P, direction);
        return true;
    }
    
    private static double Schlick(double cosine, double refIdx)
    {
        var r0 = (1 - refIdx) / (1 + refIdx);
        r0 *= r0;
        return r0 + (1 - r0) * Math.Pow(1 - cosine, 5);
    }
}