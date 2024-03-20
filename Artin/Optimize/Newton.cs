using Artin.Nuenv;
using Artin.Nuenv.Root;

namespace Artin.Optimize;

public static partial class Optimize
{
    public static double Newton(
        Func<double, double> function,
        double x0,
        double h = 1e-2,
        double atol = 1e-4,
        int maxIter = 10000
    )
    {
        Func<double, double> diffFunction = x => Diff.Differentiate(function, x, 2.0 * h);
        Func<double, double> diffSecondFunction = x => Diff.Differentiate(diffFunction, x, h);

        return Root.Newton(diffFunction, diffSecondFunction, x0, atol, maxIter);
    }
}