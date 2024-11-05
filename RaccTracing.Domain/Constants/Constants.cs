using RaccTracing.Domain.Entities;

namespace RaccTracing.Domain.Constants;

public static class Constants
{
    public const double Infinity = double.PositiveInfinity;
    public const double NegativeInfinity = double.NegativeInfinity;
    public const double Pi = 3.1415926535897932385;
    public static readonly Interval EmptyInterval = new(Infinity, NegativeInfinity);
    public static readonly Interval UniverseInterval = new(NegativeInfinity, Infinity);
    public static double DegreesToRadians(double degrees)
    {
        return degrees * Pi / 180.0;
    }
}