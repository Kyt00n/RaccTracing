using Microsoft.Extensions.DependencyInjection;
using RaccTracing.Application.Interfaces;
using RaccTracing.Infrastructure.Services;
using RaccTracing.Web;


var serviceProvider = new ServiceCollection()
    .AddSingleton<ICameraService, CameraService>()
    .AddSingleton(provider => CameraSettingsInitializer.CameraSettings)
    .AddSingleton<GraphicsGenerator>()
    .BuildServiceProvider();

var graphicsGenerator = serviceProvider.GetService<GraphicsGenerator>();

var currentDirectory = Directory.GetCurrentDirectory();
var filePath = args.Length == 0 ? Path.Combine(currentDirectory, "image.ppm") : args[0];

graphicsGenerator?.GenerateImage(filePath);