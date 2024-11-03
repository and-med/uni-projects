using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerics.Integration
{
    public interface IIntegrator
    {
        double Integrate(Func<double, double> func, double a, double b, double epselon);
        int GetSegmentsCount();
    }
}
