﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RaccTracing.Application.Interfaces;
using RaccTracing.Infrastructure.Services;
using RaccTracing.Web;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var serviceProvider = new ServiceCollection()
    .AddSingleton<IColorService, ColorService>()
    .AddSingleton<IConfiguration>(configuration)
    .AddSingleton<GraphicsGenerator>()
    .BuildServiceProvider();

var graphicsGenerator = serviceProvider.GetService<GraphicsGenerator>();

var currentDirectory = Directory.GetCurrentDirectory();
var filePath = Path.Combine(currentDirectory, "image.ppm");


graphicsGenerator?.GenerateImage(filePath);