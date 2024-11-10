using RaccTracing.Domain.Entities;
using RaccTracing.Domain.Entities.Hittable;

namespace RaccTracing.Domain.Materials;

public class Metal(Color albedo, double fuzz) : Material
{
    private Color Albedo { get; set; } = albedo;
    private double Fuzz { get; set; } = fuzz <1? fuzz : 1;

    public override bool Scatter(Ray rayIn, HitRecord hitRecord, out Vec3 attenuation, out Ray scattered)
    {
        var reflected = Vec3.Reflect(rayIn.Direction.UnitVector(), hitRecord.Normal);
        reflected += Fuzz * Vec3.RandomInUnitSphere();
        scattered = new Ray(hitRecord.P, reflected);
        attenuation = Albedo;
        return Vec3.Dot(scattered.Direction, hitRecord.Normal) > 0;
    }
}