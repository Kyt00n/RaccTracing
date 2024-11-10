using RaccTracing.Domain.Entities;
using RaccTracing.Domain.Entities.Hittable;

namespace RaccTracing.Domain.Materials;

public class Metal(Color albedo) : Material
{
    private Color Albedo { get; set; } = albedo;

    public override bool Scatter(Ray rayIn, HitRecord hitRecord, out Vec3 attenuation, out Ray scattered)
    {
        var reflected = Vec3.Reflect(rayIn.Direction.UnitVector(), hitRecord.Normal);
        scattered = new Ray(hitRecord.P, reflected);
        attenuation = Albedo;
        return true;
    }
}