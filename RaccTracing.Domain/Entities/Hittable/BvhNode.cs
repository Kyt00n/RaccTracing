namespace RaccTracing.Domain.Entities.Hittable;

public class BvhNode : Hittable
{
    private Hittable Left { get; set; }
    private Hittable Right { get; set; }
    private AxisAlignedBoundingBox Box { get; set; }

    public BvhNode(HittableList list): this(list.Objects, 0, list.Objects.Count)
    {
        
    }

    public BvhNode(List<Hittable> objects, int start, int end)
    {
        
    }

    public override bool Hit(Ray r, double tMin, double tMax, ref HitRecord rec)
    {
        throw new NotImplementedException();
    }

    public override bool Hit(Ray r, Interval rayT, ref HitRecord rec)
    {
        if (!Box.Hit(r, rayT))
        {
            return false;
        }
        
        var hitLeft = Left.Hit(r, rayT, ref rec);
        var hitRight = Right.Hit(r, new Interval(rayT.Min, hitLeft ? rec.T : rayT.Max), ref rec);
        
        return hitLeft || hitRight;
    }

    public override AxisAlignedBoundingBox BoundingBox()
    {
        return Box;
    }
}