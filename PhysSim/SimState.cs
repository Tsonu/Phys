using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysSim
{
    public class SimState : IEnumerable<Body>
    {
        List<Body> Bodies { get; set; }

        public SimState()
        {
            Bodies = new List<Body>();
        }

        public void Remove(Body b)
        {
            Bodies.Remove(b);
        }

        public Body this[int i]
        {
            get
            {
                return Bodies[i];
            }
        }

        public void Add(Body b)
        {
            b.State = this;
            Bodies.Add(b);
        }

        public int Count
        {
            get
            {
                return Bodies.Count;
            }
        }

        public IEnumerator<Body> GetEnumerator()
        {
            return Bodies.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Bodies.GetEnumerator();
        }
    }
}
