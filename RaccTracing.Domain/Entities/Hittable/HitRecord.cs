namespace RaccTracing.Domain.Entities.Hittable;

public class HitRecord
{
    public Point3 P { get; set; }
    public Vec3 Normal { get; set; }
    public double T { get; set; }
    public bool FrontFace { get; set; }
    
    public void SetFaceNormal(Ray r, Vec3 outwardNormal)
    {
        FrontFace = Vec3.Dot(r.Direction, outwardNormal) < 0;
        Normal = FrontFace ? outwardNormal : -outwardNormal;
    }
}