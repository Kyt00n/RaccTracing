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
    public double Size => Max - Min;
    public bool Contains(double value) => value >= Min && value <= Max;
    public bool Surrounds(double value) => value > Min && value < Max;
}