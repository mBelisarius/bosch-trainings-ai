namespace Artin.Nuenv;

public static class Integrate
{
    private static readonly double[] Wg =
    {
        6.667134430868813759356880989333179e-02,
        1.494513491505805931457763396576973e-01,
        2.190863625159820439955349342281632e-01,
        2.692667193099963550912269215694694e-01,
        2.955242247147528701738929946513383e-01,
        2.955242247147528701738929946513383e-01,
        2.692667193099963550912269215694694e-01,
        2.190863625159820439955349342281632e-01,
        1.494513491505805931457763396576973e-01,
        6.667134430868813759356880989333179e-02
    };

    private static readonly double[] Xg =
    {
        -9.739065285171717200779640120844521e-01,
        -8.650633666889845107320966884234930e-01,
        -6.794095682990244062343273651148736e-01,
        -4.333953941292471907992659431657842e-01,
        -1.488743389816312108848260011297200e-01,
        1.488743389816312108848260011297200e-01,
        4.333953941292471907992659431657842e-01,
        6.794095682990244062343273651148736e-01,
        8.650633666889845107320966884234930e-01,
        9.739065285171717200779640120844521e-01
    };

    private static readonly double[] Wk =
    {
        1.169463886737187427806439606219205e-02,
        3.255816230796472747881897245938976e-02,
        5.475589657435199603138130024458018e-02,
        7.503967481091995276704314091619001e-02,
        9.312545458369760553506546508336634e-02,
        1.093871588022976418992105903258050e-01,
        1.234919762620658510779581098310742e-01,
        1.347092173114733259280540017717068e-01,
        1.427759385770600807970942731387171e-01,
        1.477391049013384913748415159720680e-01,
        1.494455540029169056649364683898212e-01,
        1.477391049013384913748415159720680e-01,
        1.427759385770600807970942731387171e-01,
        1.347092173114733259280540017717068e-01,
        1.234919762620658510779581098310742e-01,
        1.093871588022976418992105903258050e-01,
        9.312545458369760553506546508336634e-02,
        7.503967481091995276704314091619001e-02,
        5.475589657435199603138130024458018e-02,
        3.255816230796472747881897245938976e-02,
        1.169463886737187427806439606219205e-02
    };

    private static readonly double[] Xk =
    {
        -9.956571630258080807355272806890028e-01,
        -9.739065285171717200779640120844521e-01,
        -9.301574913557082260012071800595083e-01,
        -8.650633666889845107320966884234930e-01,
        -7.808177265864168970637175783450424e-01,
        -6.794095682990244062343273651148736e-01,
        -5.627571346686046833390000992726941e-01,
        -4.333953941292471907992659431657842e-01,
        -2.943928627014601981311266031038656e-01,
        -1.488743389816312108848260011297200e-01,
        0.000000000000000000000000000000000e+00,
        1.488743389816312108848260011297200e-01,
        2.943928627014601981311266031038656e-01,
        4.333953941292471907992659431657842e-01,
        5.627571346686046833390000992726941e-01,
        6.794095682990244062343273651148736e-01,
        7.808177265864168970637175783450424e-01,
        8.650633666889845107320966884234930e-01,
        9.301574913557082260012071800595083e-01,
        9.739065285171717200779640120844521e-01,
        9.956571630258080807355272806890028e-01
    };

    public static double Auto(Func<double, double> func, double a, double b, double tol = 6e-6)
    {
        double aux;
        var integralG = 0.0;
        var integralK = 0.0;
        var integral = 0.0;

        for (int i = 0; i < Wg.Length; i++)
        {
            aux = 0.5 * (b - a) * (Xg[i] + 1.0) + a;
            integralG += Wg[i] * func(aux);
        }

        integralG *= (b - a) / 2.0;

        for (int i = 0; i < Wk.Length; i++)
        {
            aux = 0.5 * (b - a) * (Xk[i] + 1.0) + a;
            integralK += Wk[i] * func(aux);
        }

        integralK *= (b - a) / 2.0;

        var error = Math.Abs(integralK - integralG);

        if (error < tol)
        {
            integral += integralK;
        }
        else
        {
            var n = (int)Math.Ceiling(1.0 + Math.Log2(error / tol));
            var h = (b - a) / n;

            for (int i = 0; i < n; i++)
            {
                integral += Auto(func, a + i * h, a + (i + 1) * h, tol);
            }
        }

        return integral;
    }
}