using RaccTracing.Domain.Entities;
using RaccTracing.Domain.Entities.Hittable;

namespace RaccTracing.Domain.Materials;

public class Lambertian(Color albedo) : Material
{
    private Color Albedo { get; set; } = albedo;

    public override bool Scatter(Ray rayIn, HitRecord hitRecord, out Vec3 attenuation, out Ray scattered)
    {
        var scatterDirection = hitRecord.Normal + Vec3.RandomUnitVector();
        
        if (scatterDirection.NearZero())
        {
            scatterDirection = hitRecord.Normal;
        }
        
        scattered = new Ray(hitRecord.P, scatterDirection);
        attenuation = Albedo;
        return true;
    }
}