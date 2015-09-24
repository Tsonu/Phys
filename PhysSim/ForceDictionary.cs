using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysSim
{
    public class ForceDictionary
    {
        public static List<IForce> Forces { get; private set; }

        static ForceDictionary()
        {
            Forces = new List<IForce>();
            Forces.Add(new ElasticCollision());
            Forces.Add(new Gravity());
            Forces.Add(new EM());
            
        }
    }
}
