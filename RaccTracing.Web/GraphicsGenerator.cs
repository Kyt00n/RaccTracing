using System.Text;
using Microsoft.Extensions.Configuration;
using RaccTracing.Application.Interfaces;
using RaccTracing.Domain.Configuration;
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
        
        var materialGround = new Lambertian(new Color(0.8, 0.8, 0.0));
        var materialCenter = new Lambertian(new Color(0.1, 0.2, 0.5));
        var materialLeft = new Dielectric(1.5);
        var materialBubble = new Dielectric(1.0/1.5);
        var materialRight = new Metal(new Color(0.8, 0.6, 0.2), 1.0);
        
        world.Add(new Sphere(new Point3(0, 0, -1.2), 0.5, materialCenter));
        world.Add(new Sphere(new Point3(0, -100.5, -1), 100, materialGround));
        world.Add(new Sphere(new Point3(-1, 0, -1), 0.5, materialLeft));
        world.Add(new Sphere(new Point3(-1, 0, -1), -0.45, materialBubble));
        world.Add(new Sphere(new Point3(1, 0, -1), 0.5, materialRight));
        
        cameraService.Render(sb, world);
        
        File.WriteAllText(filePath, sb.ToString());
        Console.WriteLine($"Output saved to {filePath}");
    }
}