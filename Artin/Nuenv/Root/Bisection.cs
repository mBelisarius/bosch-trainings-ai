namespace Artin.Nuenv.Root;

public static partial class Root
{
    public static double Bisection(
        Func<double, double> function,
        double a,
        double b,
        double rtol = 1e-4,
        double atol = 1e-4,
        int maxIter = 1000)
    {
        double c = 0.0;

        for (int i = 0; i < maxIter; i++)
        {
            c = (a + b) / 2.0;
            var fa = function(a);
            var fc = function(c);

            if (fa * fc < 0.0)
                b = c;
            else
                a = c;

            // Interrupt by absolute tolerance
            if (Math.Abs(fc) < atol)
                break;

            // Interrupt by relative tolerance
            if (b - a < 2.0 * rtol)
                break;
        }

        return c;
    }
}