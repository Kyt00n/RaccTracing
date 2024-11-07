using System.Text;
using Microsoft.Extensions.Configuration;
using RaccTracing.Application.Interfaces;
using RaccTracing.Domain.Configuration;
using RaccTracing.Domain.Entities;
using RaccTracing.Domain.Entities.Hittable;
using RaccTracing.Domain.Entities.Objects;

namespace RaccTracing.Web;

public class GraphicsGenerator(ICameraService cameraService)
{
    public void GenerateImage(string filePath)
    {
        StringBuilder sb = new();
        
        HittableList world = new();
        world.Add(new Sphere(new Point3(0, 0, -1), 0.5));
        world.Add(new Sphere(new Point3(0, -100.5, -1), 100));
        
        cameraService.Render(sb, world);
        
        File.WriteAllText(filePath, sb.ToString());
        Console.WriteLine($"Output saved to {filePath}");
    }
}