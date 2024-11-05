using System.Text;
using Moq;
using RaccTracing.Application.Interfaces;
using RaccTracing.Domain.Entities;
using RaccTracing.Domain.Entities.Hittable;
using RaccTracing.Infrastructure.Services;
using Xunit;

namespace RaccTracingTests;

public class CameraServiceTests
{
    private readonly CameraService _cameraService;
    private readonly Mock<Hittable> _mockWorld;
    private readonly CameraSettings _cameraSettings;

    public CameraServiceTests()
    {
        _cameraService = new CameraService();
        _mockWorld = new Mock<Hittable>();
        _cameraSettings = new CameraSettings
        {
            ImageWidth = 100,
            AspectRatio = 1.7778,
            FocalLength = 1.0,
            ViewportHeight = 2.0,
            CameraCenter = new Point3(0, 0, 0)
        };
    }

    [Fact]
    public void Render_ShouldGenerateImage()
    {
        // Arrange
        var output = new StringBuilder();

        // Act
        _cameraService.Render(output, _mockWorld.Object, _cameraSettings);
        // Assert
        Assert.Contains("P3", output.ToString());
        Assert.Contains("255", output.ToString());
    }

    
}