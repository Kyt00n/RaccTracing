using Constant = RaccTracing.Domain.Constants.Constants;
namespace RaccTracing.Domain.Entities;
public class Vec3()
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Vec3(double x, double y, double z) : this()
    {
        X = x;
        Y = y;
        Z = z;
    }
    private const double Tolerance = 1e-10;

    //Equality
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        var v = (Vec3)obj;
        return Math.Abs(X - v.X) < Tolerance &&
               Math.Abs(Y - v.Y) < Tolerance &&
               Math.Abs(Z - v.Z) < Tolerance;    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }
    
    public static bool operator ==(Vec3 a, Vec3 b)
    {
        return a.Equals(b);
    }
    
    public static bool operator !=(Vec3 a, Vec3 b)
    {
        return !a.Equals(b);
    }
    
    //operations
    public static Vec3 operator -(Vec3 a)
    {
        return new Vec3(-a.X, -a.Y, -a.Z);
    }
    
    public static Vec3 operator +(Vec3 a, Vec3 b)
    {
        return new Vec3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }
    
    public static Vec3 operator -(Vec3 a, Vec3 b)
    {
        return new Vec3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }
    
    public static Vec3 operator *(Vec3 a, double b)
    {
        return new Vec3(a.X * b, a.Y * b, a.Z * b);
    }
    
    public static Vec3 operator *(double a, Vec3 b)
    {
        return new Vec3(a * b.X, a * b.Y, a * b.Z);
    }
    
    public static Vec3 operator /(Vec3 a, double b)
    {
        return new Vec3(a.X / b, a.Y / b, a.Z / b);
    }
    
    public static Vec3 operator /(double a, Vec3 b)
    {
        return new Vec3(a / b.X, a / b.Y, a / b.Z);
    }
    
    public static double Dot(Vec3 a, Vec3 b)
    {
        return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
    }
    
    public static Vec3 Cross(Vec3 a, Vec3 b)
    {
        return new Vec3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
    }
    
    public double Length()
    {
        return Math.Sqrt(LengthSquared());
    }
    
    public double LengthSquared()
    {
        return X * X + Y * Y + Z * Z;
    }
    
    public Vec3 UnitVector()
    {
        return this / Length();
    }

    public static Vec3 RandomOnHemisphere(Vec3 normal)
    {
        var onUnitSphere = RandomUnitVector();
        if (Dot(onUnitSphere, normal) > 0.0)
        {
            return onUnitSphere;
        }
        return -onUnitSphere;
    }

    public static Vec3 RandomUnitVector()
    {
        return RandomInUnitSphere().UnitVector();
    }

    private static Vec3 RandomInUnitSphere()
    {
        while (true)
        {
            var p = Random(-1, 1);
            if (p.LengthSquared() >= 1)
            {
                continue;
            }
            return p;
        }
    }
    public static Vec3 Random()
    {
        return new Vec3(Constant.RandomDouble(), Constant.RandomDouble(), Constant.RandomDouble());
    }
    
    public static Vec3 Random(double min, double max)
    {
        return new Vec3(Constant.RandomDouble(min, max), Constant.RandomDouble(min, max), Constant.RandomDouble(min, max));
    }
    
    public double this[int i]
    {
        get
        {
            return i switch
            {
                0 => X,
                1 => Y,
                2 => Z,
                _ => throw new IndexOutOfRangeException("Index must be 0, 1, or 2")
            };
        }
        set
        {
            _ = i switch
            {
                0 => X = value,
                1 => Y = value,
                2 => Z = value,
                _ => throw new IndexOutOfRangeException("Index must be 0, 1, or 2")
            };
        }
    }
}