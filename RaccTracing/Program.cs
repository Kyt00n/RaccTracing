using System.Text;

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
        var red = (double)i / (image_width - 1);
        var green = (double)j / (image_height - 1);
        var blue = 0.0;

        var ir = (int)(255.999 * red);
        var ig = (int)(255.999 * green);
        var ib = (int)(255.999 * blue);

        sb.AppendLine($"{ir} {ig} {ib}");
    }
}
Console.WriteLine("Done");

File.WriteAllText(filePath, sb.ToString());
Console.WriteLine($"Output saved to {filePath}");