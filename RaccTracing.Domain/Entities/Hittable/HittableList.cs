namespace RaccTracing.Domain.Entities.Hittable;

public class HittableList : Hittable
{
    List<Hittable> Objects { get; set; }
    
    public HittableList()
    {
        Objects = new List<Hittable>();
    }
    
    public void Add(Hittable hittable)
    {
        Objects.Add(hittable);
    }

    public void Clear()
    {
        Objects.Clear();
    }

    public override bool Hit(Ray r, double tMin, double tMax, ref HitRecord rec)
    {
        var tempRec = new HitRecord();
        var hitAnything = false;
        var closestSoFar = tMax;
        foreach (var obj in Objects.Where(obj => obj.Hit(r, tMin, closestSoFar, ref tempRec)))
        {
            hitAnything = true;
            closestSoFar = tempRec.T;
            rec = tempRec;
        }
        return hitAnything;
    }

    public override bool Hit(Ray r, Interval rayT, ref HitRecord rec)
    {
        var tempRec = new HitRecord();
        var hitAnything = false;
        var closestSoFar = rayT.Max;
        foreach (var obj in Objects.Where(obj => obj.Hit(r, rayT.Min, closestSoFar, ref tempRec)))
        {
            hitAnything = true;
            closestSoFar = tempRec.T;
            rec = tempRec;
        }
        return hitAnything;
    }
}