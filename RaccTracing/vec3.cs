namespace RaccTracing;

public class Vec3(double element0, double element1, double element2)
{
    private double _element0 = element0;
    private double _element1 = element1;
    private double _element2 = element2;

    public Vec3() : this(0, 0, 0)
    {
    }

    public double X() => _element0;
    public double Y() => _element1;
    public double Z() => _element2;
    
    //operations
    public static Vec3 operator -(Vec3 a)
    {
        return new Vec3(-a._element0, -a._element1, -a._element2);
    }

    public static Vec3 operator +(Vec3 a, Vec3 b)
    {
        return new Vec3(a.X() + b.X(), a.Y() + b.Y(), a.Z() + b.Z());
    }
    public static Vec3 operator -(Vec3 a, Vec3 b)
    {
        return new Vec3(a.X() - b.X(), a.Y() - b.Y(), a.Z() - b.Z());
    }
    
}