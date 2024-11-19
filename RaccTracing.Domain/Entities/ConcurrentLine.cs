using System.Text;

namespace RaccTracing.Domain.Entities;

public class ConcurrentLine(int lineNumber, StringBuilder lineValues)
{
    public int LineNumber { get; set; } = lineNumber;
    private StringBuilder LineValues { get; set; } = lineValues;

    public override string ToString()
    {
        return LineValues.ToString();
    }
}