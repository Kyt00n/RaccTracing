using RaccTracing.Domain.Entities;

namespace RaccTracing.Domain.Constants;

public static class Constants
{
    
    public const double Infinity = double.PositiveInfinity;
    public const double NegativeInfinity = double.NegativeInfinity;
    public const double Pi = 3.1415926535897932385;
    public static readonly Interval EmptyInterval = new(Infinity, NegativeInfinity);
    public static readonly Interval UniverseInterval = new(NegativeInfinity, Infinity);
    private static readonly Random Random = new();
    public static double DegreesToRadians(double degrees)
    {
        return degrees * Pi / 180.0;
    }

    public static double RandomDouble()
    {
        return Random.NextDouble();
    }
    
    public static double RandomDouble(double min, double max)
    {
        return min + (max - min) * Random.NextDouble();
    }

    public static Vec3 RandomInUnitDisk()
    {
        while (true)
        {
            var p = new Vec3(RandomDouble(-1, 1), RandomDouble(-1, 1), 0);
            if (p.LengthSquared() < 1) return p;
        }
    }
}