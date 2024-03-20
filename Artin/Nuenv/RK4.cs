namespace Artin.Nuenv;

public struct Solution
{
    public readonly double[]   T;
    public readonly double[][] X;
    public readonly int        Size;

    public Solution(double[] t, double[][] x, int size)
    {
        T = t;
        X = x;
        Size = size;
    }
}

public class Rk4
{
    private readonly Func<double, double[], double[]> _ode;
    private          double[]                         _k1, _k2, _k3, _k4;

    private const double C2 = 1.0 / 2.0;
    private const double C3 = 1.0 / 2.0;

    private const double A21 = 1.0 / 2.0;

    private const double A31 = 0.0;
    private const double A32 = 1.0 / 2.0;

    private const double A41 = 0.0;
    private const double A42 = 0.0;
    private const double A43 = 1.0;

    private const double B1 = 1.0 / 6.0;
    private const double B2 = 1.0 / 3.0;
    private const double B3 = 1.0 / 3.0;
    private const double B4 = 1.0 / 6.0;

    public Rk4(Func<double, double[], double[]> ode)
    {
        _ode = ode ?? throw new ArgumentNullException(nameof(ode));

        _k1 = null!;
        _k2 = null!;
        _k3 = null!;
        _k4 = null!;
    }

    public double[] Iterate(double t0, double[] x0, double step)
    {
        _k1 = _ode(t0, x0);
        _k2 = _ode(t0 + C2 * step, x0.Select((x, i) => x + step * (A21 * _k1[i])).ToArray());
        _k3 = _ode(t0 + C3 * step, x0.Select((x, i) => x + step * (A31 * _k1[i] + A32 * _k2[i])).ToArray());
        _k4 = _ode(t0 + step, x0.Select((x, i) => x + step * (A41 * _k1[i] + A42 * _k2[i] + A43 * _k3[i])).ToArray());

        return x0.Select((x, i) => x + step * (B1 * _k1[i] + B2 * _k2[i] + B3 * _k3[i] + B4 * _k4[i])).ToArray();
    }

    public Solution Solve(double[] tEval, double[] x0, Func<double[], bool>? stopEvent = null)
    {
        stopEvent ??= _ => false;

        var size = tEval.Length;
        var x = new double[size][];
        x[0] = x0;

        int i = 1;
        for (; i < size; i++)
        {
            var step = tEval[i] - tEval[i - 1];
            x[i] = Iterate(tEval[i - 1], x[i - 1], step);

            if (stopEvent(x[i]))
            {
                i++;
                break;
            }
        }

        return new Solution(tEval.Take(i).ToArray(), x.Take(i).ToArray(), i);
    }
}