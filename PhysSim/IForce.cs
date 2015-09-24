using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysSim
{
    public interface IForce
    {
        void ApplyForce(ref Body b1, ref Body b2, double deltaT);
    }
}
