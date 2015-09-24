using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysSim
{
    public class Gravity : IForce
    {
        static double G = 6.67384e-11f;

        public void ApplyForce(ref Body b1, ref Body b2, double deltaT)
        {
            var direction = b1.DirectionTo(b2);
            var dist = direction.Magnitude;
            var f = G * b1.Mass * b2.Mass / (dist * dist);
            var dirNorm = direction.UnitVector;


            b1.Vel -= f * deltaT / b1.Mass * dirNorm;
            b2.Vel += f * deltaT / b2.Mass * dirNorm;

        }
    }
}
