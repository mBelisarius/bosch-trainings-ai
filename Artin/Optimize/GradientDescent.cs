using Artin.Nuenv;

namespace Artin.Optimize;

public static partial class Optimize
{
    public static double GradientDescent(
        Func<double, double> function,
        double x0,
        double lr = 1e-2,
        double atol = 1e-4
    )
    {
        var xp = x0;
        var diff = Diff.Differentiate(function, xp);

        while (Math.Abs(diff) > atol)
        {
            diff =  Diff.Differentiate(function, xp);
            xp   -= lr * diff;
        }

        return xp;
    }
}