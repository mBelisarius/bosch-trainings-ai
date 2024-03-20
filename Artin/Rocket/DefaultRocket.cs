using Artin.Nuenv.Interp1D;

namespace Artin.Rocket;

public class DefaultRocket : BaseRocket
{
    private readonly double   _vExhaust;
    private readonly double   _cd0;
    private readonly double[] _timeData;
    private readonly double[] _massFlowData;

    public DefaultRocket(
        double dryMass,
        double crossArea,
        double engineTime,
        double vExhaust,
        double cd0,
        double[] timeData,
        double[] massFlowData
    )
        : base(dryMass, crossArea, engineTime)
    {
        _vExhaust     = vExhaust;
        _cd0          = cd0;
        _timeData     = timeData ?? throw new ArgumentNullException(nameof(timeData));
        _massFlowData = massFlowData ?? throw new ArgumentNullException(nameof(massFlowData));
    }

    protected override double MassFlowCurve(double t)
    {
        return Interp1D.Linear(_timeData, _massFlowData, t);
    }

    protected override double ThrustCurve(double t, double pExt)
    {
        return MassFlowCurve(t) * _vExhaust;
    }

    protected override double DragCurve(double v, double h)
    {
        var temp = Atmosphere.Temperature(h);
        return Drag.Coefficient(v, temp, _cd0);
    }

    protected override double PressureCurve(double h)
    {
        return Atmosphere.Pressure(h);
    }

    protected override double DensityCurve(double h)
    {
        return Atmosphere.Density(h);
    }

    protected override double GravityCurve(double h)
    {
        return Gravity.GetGravity(h);
    }
}