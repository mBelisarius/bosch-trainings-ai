namespace Artin.Nuenv.Interp1D;

public static partial class Interp1D
{
    public static double Exponential(double[] x, double[] y, double query, bool checkBounds = true)
    {
        if (x.Length != y.Length)
        {
            throw new ArgumentException("Arrays 'x' and 'y' must have the same size");
        }

        if (checkBounds && (query < x[0] || query > x[^1]))
        {
            throw new ArgumentOutOfRangeException(nameof(query), $"query {query} is out of bounds");
        }

        var index = Search.Search.BinarySearchLower(x, query);

        var zeta = Math.Log(y[index] / y[index - 1]) / (x[index] - x[index - 1]);

        return y[index - 1] * Math.Exp(zeta * (query - x[index - 1]));
    }
}