namespace Artin.Nuenv.Root;

public static partial class Root
{
    public static double Newton(
        Func<double, double> function,
        Func<double, double> der,
        double x0,
        double atol = 1e-4,
        int maxIter = 10000
    )
    {
        double xp = x0;

        for (int i = 0; i < maxIter; i++)
        {
            var fp = function(xp);
            xp -= fp / der(xp);

            if (Math.Abs(fp) < atol)
                break;
        }

        return xp;
    }
}