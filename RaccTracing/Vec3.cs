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
    
    //operations on []
    public double this[int i]
    {
        get
        {
            return i switch
            {
                0 => _element0,
                1 => _element1,
                2 => _element2,
                _ => throw new IndexOutOfRangeException("Index must be 0, 1, or 2")
            };
        }
        set
        {
            switch (i)
            {
                case 0:
                    _element0 = value;
                    break;
                case 1:
                    _element1 = value;
                    break;
                case 2:
                    _element2 = value;
                    break;
                default:
                    throw new IndexOutOfRangeException("Index must be 0, 1, or 2");
            }
        }
    }
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

    public static Vec3 operator *(Vec3 v, double t)
    {
        v[0] *= t;
        v[1] *= t;
        v[2] *= t;
        return v;
    }
    public static Vec3 operator /(Vec3 v, double t)
    {
        return v *= 1 / t;
    }

    public double Length => Math.Sqrt(LengthSquared);
    public double LengthSquared => _element0 * _element0 + _element1 * _element1 + _element2 * _element2;

    public override string ToString()
    {
        return $"{_element0} {_element1} {_element2}";
    }

    public override bool Equals(object? obj)
    {
        var item = obj as Vec3;

        if (item == null)
        {
            return false;
        }

        return _element0.Equals(item._element0) && _element1.Equals(item._element1) &&
               _element2.Equals(item._element2);
    }
}