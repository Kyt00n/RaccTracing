using System.Text;
using RaccTracing.Application.Interfaces;
using RaccTracing.Domain.Entities;

namespace RaccTracing.Web;

public class GraphicsGenerator
{
    private readonly IColorService _colorService;

    public GraphicsGenerator(IColorService colorService)
    {
        _colorService = colorService;
    }

    public void GenerateImage(string filePath, int imageWidth, int imageHeight)
    {
        StringBuilder sb = new();
        sb.Append($"P3\n{imageWidth} {imageHeight}\n255\n");

        for (int j = 0; j < imageHeight; j++)
        {
            Console.WriteLine($"Scan lines remaining: {imageHeight - j}");
            for (int i = 0; i < imageWidth; i++)
            {
                var pixelColor = new Vec3((double)i / (imageWidth - 1), (double)j / (imageHeight - 1), 0.0);
                _colorService.WriteColor(sb, pixelColor);
            }
        }

        Console.WriteLine("Done");
        File.WriteAllText(filePath, sb.ToString());
        Console.WriteLine($"Output saved to {filePath}");
    }
}