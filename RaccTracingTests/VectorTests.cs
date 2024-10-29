using RaccTracing;

namespace RaccTracingTests;

public class VectorTests
{
    
    [Fact]
    public void TestVectorInitialization()
    {
        var vector = new Vec3(1, 2, 3);
        Assert.Equal(1, vector[0]);
        Assert.Equal(2, vector[1]);
        Assert.Equal(3, vector[2]);
    }

    [Fact]
    public void TestVectorElementAssignment()
    {
        var vector = new Vec3(1, 2, 3)
        {
            [1] = 3
        };
        Assert.Equal(1, vector[0]);
        Assert.Equal(3, vector[1]);
        Assert.Equal(3, vector[2]);
    }

    [Fact]
    public void TestVectorAddition()
    {
        var vector1 = new Vec3(1, 2, 3);
        var vector2 = new Vec3(1, 2, 4);
        var result = vector1 + vector2;
        Assert.Equal(new Vec3(2, 4, 7), result);
    }

    [Fact]
    public void TestVectorSubtraction()
    {
        var vector1 = new Vec3(1, 2, 3);
        var vector2 = new Vec3(1, 2, 4);
        var result = vector2 - vector1;
        Assert.Equal(new Vec3(0, 0, 1), result);
    }

    [Fact]
    public void TestVectorMultiplication()
    {
        var vector = new Vec3(1, 2, 4);
        var result = vector * 3;
        Assert.Equal(new Vec3(3, 6, 12), result);
    }
}