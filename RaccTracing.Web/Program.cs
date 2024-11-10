using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RaccTracing.Application.Interfaces;
using RaccTracing.Infrastructure.Services;
using RaccTracing.Web;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var serviceProvider = new ServiceCollection()
    .AddSingleton<ICameraService, CameraService>()
    .AddSingleton<IConfiguration>(configuration)
    .AddSingleton<CameraSettingsInitializer>()
    .AddSingleton(provider => provider.GetRequiredService<CameraSettingsInitializer>().CameraSettings)
    .AddSingleton<GraphicsGenerator>()
    .BuildServiceProvider();

var graphicsGenerator = serviceProvider.GetService<GraphicsGenerator>();

var currentDirectory = Directory.GetCurrentDirectory();
var filePath = args.Length == 0 ? Path.Combine(currentDirectory, "image.ppm") : args[0];

graphicsGenerator?.GenerateImage(filePath);