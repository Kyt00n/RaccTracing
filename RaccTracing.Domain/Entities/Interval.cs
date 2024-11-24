namespace RaccTracing.Domain.Entities;

public class Interval
{
    public double Min { get; set; }
    public double Max { get; set; }
    
    public Interval()
    {
        Min = Constants.Constants.NegativeInfinity;
        Max = Constants.Constants.Infinity;
    }
    public Interval(double min, double max)
    {
        Min = min;
        Max = max;
    }
    public Interval(Interval a, Interval b)
    {
        Min = Math.Min(a.Min, b.Min);
        Max = Math.Max(a.Max, b.Max);
    }
    public double Size => Max - Min;
    public bool Contains(double value) => value >= Min && value <= Max;
    public bool Surrounds(double value) => value > Min && value < Max;
    public double Clamp(double value) => Math.Max(Min, Math.Min(Max, value));

    public Interval Expand(double delta)
    {
        var padding = delta / 2;
        return new Interval
        {
            Min = Min - padding,
            Max = Max + padding
        };
    }
}