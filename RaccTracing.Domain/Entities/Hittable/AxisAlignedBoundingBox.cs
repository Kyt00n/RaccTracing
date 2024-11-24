namespace RaccTracing.Domain.Entities.Hittable;

public class AxisAlignedBoundingBox
{
    public Interval X { get; set; }
    public Interval Y { get; set; }
    public Interval Z { get; set; }
    
    public AxisAlignedBoundingBox()
    {
        X = new Interval();
        Y = new Interval();
        Z = new Interval();
    }
    public AxisAlignedBoundingBox(Point3 a, Point3 b)
    {
        X = (a.X <= b.X) ? new Interval(a.X, b.X) : new Interval(b.X, a.X);
        Y = (a.Y <= b.Y) ? new Interval(a.Y, b.Y) : new Interval(b.Y, a.Y);
        Z = (a.Z <= b.Z) ? new Interval(a.Z, b.Z) : new Interval(b.Z, a.Z);
    }
    public AxisAlignedBoundingBox(AxisAlignedBoundingBox box0, AxisAlignedBoundingBox box1)
    {
        X = new Interval(box0.X, box1.X);
        Y = new Interval(box0.Y, box1.Y);
        Z = new Interval(box0.Z, box1.Z);
    }

    private Interval AxisInterval(int n)
    {
        return n switch
        {
            1 => Y,
            2 => Z,
            _ => X
        };
    }

    public bool Hit(Ray ray, Interval rayT)
    {
        var rayOrigin = ray.Origin;
        var rayDirection = ray.Direction;
        
        for (var axis=0; axis<3; axis++)
        {
            var ax = AxisInterval(axis);
            var adinv = 1.0 / rayDirection[axis];
            
            var t0 = (ax.Min - rayOrigin[axis]) * adinv;
            var t1 = (ax.Max - rayOrigin[axis]) * adinv;

            if (t0 < t1)
            {
                if (t0 > rayT.Min) rayT.Min = t0;
                if (t1 < rayT.Max) rayT.Max = t1;
            }
            else
            {
                if (t1>rayT.Min) rayT.Min = t1;
                if (t0 < rayT.Max) rayT.Max = t0;
            }
            if (rayT.Max <= rayT.Min)
            {
                return false;
            }
        }
        return true;
    }
}