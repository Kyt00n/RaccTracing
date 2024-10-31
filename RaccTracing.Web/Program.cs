using Microsoft.Extensions.DependencyInjection;
using RaccTracing.Application.Interfaces;
using RaccTracing.Infrastructure.Services;
using RaccTracing.Web;

const int imageWidth = 256;
const int imageHeight = 256;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IColorService, ColorService>()
    .AddSingleton<GraphicsGenerator>()
    .BuildServiceProvider();

var graphicsGenerator = serviceProvider.GetService<GraphicsGenerator>();

var currentDirectory = Directory.GetCurrentDirectory();
var filePath = Path.Combine(currentDirectory, "image.ppm");


graphicsGenerator?.GenerateImage(filePath, imageWidth, imageHeight);