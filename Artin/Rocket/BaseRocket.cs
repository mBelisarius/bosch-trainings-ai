using Artin.Nuenv;

namespace Artin.Rocket;

public abstract class BaseRocket
{
    protected readonly double DryMass;
    protected readonly double CrossArea;
    protected readonly double EngineTime;

    public BaseRocket(double dryMass, double crossArea, double engineTime)
    {
        DryMass    = dryMass;
        CrossArea  = crossArea;
        EngineTime = engineTime;
    }

    protected abstract double MassFlowCurve(double t);
    protected abstract double ThrustCurve(double t, double pExt);
    protected abstract double DragCurve(double v, double h);
    protected abstract double PressureCurve(double h);
    protected abstract double DensityCurve(double h);
    protected abstract double GravityCurve(double h);

    protected double CalculateMass(double t)
    {
        if (t > EngineTime)
            return DryMass;

        // Calculate mass using numerical integration
        var mass = DryMass + Integrate.Auto(MassFlowCurve, t, EngineTime);
        
        return mass;
    }

    protected double CalculateThrust(double t, double h)
    {
        if (t > EngineTime)
            return 0.0;
        
        var pExt = PressureCurve(h);
        return ThrustCurve(t, pExt);
    }

    protected double CalculateDrag(double v, double h)
    {
        var cd = DragCurve(v, h);
        var rho = DensityCurve(h);

        var dir = (v == 0.0) ? 1.0 : v / Math.Abs(v);
        var drag = -dir * 0.5 * CrossArea * cd * rho * Math.Pow(v, 2);

        return drag;
    }

    protected double CalculateWeight(double h, double mass)
    {
        var gravity = GravityCurve(h);
        return -gravity * mass;
    }

    public double[] MomentumEq(double t, double[] y)
    {
        var mass = CalculateMass(t);
        var thrust = CalculateThrust(t, y[0]);
        var drag = CalculateDrag(y[1], y[0]);
        var weight = CalculateWeight(y[0], mass);

        var u1 = y[1];
        var u2 = (thrust + drag + weight) / mass;
        
        // Console.WriteLine($"t {t:E6} | y[0] {y[0]:E6} | y[1]=u1 {y[1]:E6} | u2 {u2:E6} | mass {mass:E6} | thrust {thrust:E6} | drag {drag:E6} | weight {weight:E6}");

        return new[] { u1, u2 };
    }
}