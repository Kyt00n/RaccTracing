using RaccTracing.Domain.Entities;

namespace RaccTracing.Domain.ColorConstants;

public static class Colors
{
    public static Color White { get; } = new(1.0, 1.0, 1.0);
    public static Color Blue { get; } = new(0.0, 0.0, 1.0);
    public static Color LightBlue { get; } = new(0.5, 0.7, 1.0);
    public static Color Red { get; } = new(1.0, 0.0, 0.0);
    public static Color Green { get; } = new(0.0, 1.0, 0.0);
    public static Color Black { get; } = new(0.0, 0.0, 0.0);
}