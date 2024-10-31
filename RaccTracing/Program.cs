using System.Text;
using RaccTracing;

var currentDirectory = Directory.GetCurrentDirectory();
var filePath = Path.Combine(currentDirectory, "image.ppm");
var image_width = 256;
var image_height = 256;

//Render
StringBuilder sb = new();
sb.Append($"P3\n{image_width} {image_height}\n255\n");

for (int j = 0; j < image_height; j++)
{
    Console.WriteLine($"Scan lines remaining: {image_height-j}");
    for (int i = 0; i < image_width; i++)
    {
        var pixelColor = new Color((double)i / (image_width - 1), (double)j / (image_height - 1), 0.0);
        ColorFunctions.WriteColor(sb, pixelColor);
    }
}
Console.WriteLine("Done");
File.WriteAllText(filePath, sb.ToString());
Console.WriteLine($"Output saved to {filePath}");