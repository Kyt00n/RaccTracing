using System.Text;
using RaccTracing.Domain.Entities;
using RaccTracing.Domain.Entities.Hittable;

namespace RaccTracing.Application.Interfaces;

public interface IColorService
{
    void WriteColor(StringBuilder output, Vec3 pixelColor);
    Color RayColor(Ray r, Hittable world);
}