using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PhysSim
{
    public class ElasticCollision : IForce
    {
        readonly double CollisionDepth = 1.01;
        readonly double AbsorbDepth = 0.75;

        public void ApplyForce(ref Body b1, ref Body b2, double deltaT)
        {
            if (b1.DistanceTo(b2) > (b1.Radius + b2.Radius) * CollisionDepth)
                return;

            if (b1.DistanceTo(b2) < (b1.Radius + b2.Radius) * AbsorbDepth)
            {
                if (b1.Mass > b2.Mass)
                {
                    b1.Mass += b2.Mass;
                    b1.Radius = Math.Sqrt(b1.Radius*b1.Radius + b2.Radius* b2.Radius);
                    b2.Delete();
                }
                else
                {
                    b2.Mass += b1.Mass;
                    b1.Radius = Math.Sqrt(b1.Radius * b1.Radius + b2.Radius * b2.Radius);
                    b1.Delete();
                }
            }

            var b1dot = (b1.Vel - b2.Vel) * (b1.Pos - b2.Pos);
            var b1mag = (b1.Pos - b2.Pos).Magnitude;
            var b1DotNormal = b1dot / (b1mag * b1mag);

            var v1delta  = (b1.Pos - b2.Pos) * (2 * b2.Mass / (b1.Mass + b2.Mass)) * b1DotNormal;


            var b2dot = (b2.Vel - b1.Vel) * (b2.Pos - b1.Pos);
            var b2mag = (b2.Pos - b1.Pos).Magnitude;
            var b2DotNormal = b2dot / (b2mag * b2mag);

            var v2Delta = (b2.Pos - b1.Pos) * (2 * b1.Mass / (b1.Mass + b2.Mass)) * b2DotNormal;
            
            b1.Vel -= v1delta;
            b2.Vel -= v2Delta;
        }
    }
}
