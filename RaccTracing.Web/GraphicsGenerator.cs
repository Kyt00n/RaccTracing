using System.Text;
using Microsoft.Extensions.Configuration;
using RaccTracing.Application.Interfaces;
using RaccTracing.Domain.Configuration;
using RaccTracing.Domain.Constants;
using RaccTracing.Domain.Entities;
using RaccTracing.Domain.Entities.Hittable;
using RaccTracing.Domain.Entities.Objects;
using RaccTracing.Domain.Materials;

namespace RaccTracing.Web;

public class GraphicsGenerator(ICameraService cameraService)
{
    public void GenerateImage(string filePath)
    {
        StringBuilder sb = new();
        
        HittableList world = new();
        var groundMaterial = new Lambertian(new Color(0.5, 0.5, 0.5));
        world.Add(new Sphere(new Point3(0, -1000, 0), 1000, groundMaterial));

        var random = new Random();

        for (int a = -11; a < 11; a++)
        {
            for (int b = -11; b < 11; b++)
            {
                var chooseMat = random.NextDouble();
                var center = new Point3(a + 0.9 * random.NextDouble(), 0.2, b + 0.9 * random.NextDouble());

                if ((center - new Point3(4, 0.2, 0)).Length() > 0.9)
                {
                    Material sphereMaterial;

                    if (chooseMat < 0.8)
                    {
                        // diffuse
                        var albedo = Color.Random() * Color.Random();
                        sphereMaterial = new Lambertian(albedo);
                        world.Add(new Sphere(center, 0.2, sphereMaterial));
                    }
                    else if (chooseMat < 0.95)
                    {
                        // metal
                        var albedo = Color.Random(0.5, 1);
                        var fuzz = random.NextDouble() * 0.5;
                        sphereMaterial = new Metal(albedo, fuzz);
                        world.Add(new Sphere(center, 0.2, sphereMaterial));
                    }
                    else
                    {
                        // glass
                        sphereMaterial = new Dielectric(1.5);
                        world.Add(new Sphere(center, 0.2, sphereMaterial));
                    }
                }
            }
        }
        
        var material1 = new Dielectric(1.5);
        world.Add(new Sphere(new Point3(0, 1, 0), 1.0, material1));

        var material2 = new Lambertian(new Color(0.4, 0.2, 0.1));
        world.Add(new Sphere(new Point3(-4, 1, 0), 1.0, material2));

        var material3 = new Metal(new Color(0.7, 0.6, 0.5), 0.0);
        world.Add(new Sphere(new Point3(4, 1, 0), 1.0, material3));
        cameraService.Render(sb, world);
        
        File.WriteAllText(filePath, sb.ToString());
        Console.WriteLine($"Output saved to {filePath}");
    }
}