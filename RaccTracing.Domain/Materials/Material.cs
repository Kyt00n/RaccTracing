using RaccTracing.Domain.Entities;
using RaccTracing.Domain.Entities.Hittable;

namespace RaccTracing.Domain.Materials;

public abstract class Material
{
    public abstract bool Scatter(Ray rayIn, HitRecord hitRecord, out Vec3 attenuation, out Ray scattered);
    
}