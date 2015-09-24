using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PhysSim
{
    public class Simulation
    {
        public SimState State { get; private set; }
        public Bounds SimBounds { get; private set; }
        public Bounds ViewBounds { get; private set; }
        public int ViewFactor { get; private set; }
        public double Acceleration { get; private set; }
        public double Scale { get; private set; }
        public int Speed { get; private set; }

        public Simulation()
        {
            Scale = 1e9;
            ViewFactor = 2;

            SimBounds = new Bounds() { MinX = -Scale, MinY = -Scale, MaxX = Scale, MaxY = Scale };
            ViewBounds = new Bounds() { MinX = -Scale * ViewFactor, MinY = -Scale * ViewFactor, MaxX = Scale * ViewFactor, MaxY = Scale * ViewFactor };
            Acceleration = 1000;
            Speed = 20;

            Random r = new Random();
            State = new SimState();

            for (int i = 0; i < 100; i++)
            {
                State.Add(Body.Earth);
                //State.Add(Body.Moon);
                State[State.Count - 1].Pos = Vector2D.Random(r, Scale);
               // State[State.Count - 2].Pos = Vector2D.Random(r, Scale);
            }
           // State.Add(Body.BlackHole);
            //State.Add(Body.Earth);
            ////State.Add(Body.Moon);
            //State.Add(Body.Earth);
            //State.Add(Body.Earth);
            //State[0].Pos.X = 4e8f;
            //State[0].Pos.Y = 1e4f;
            //State[0].Vel.X = -1e5f;

        }

        public void Step()
        {
            for (int i = 0; i < Speed; i++)
            {
                StepInternal();
            }
        }

        public void StepInternal()
        {
            var deltaT = 1 / 60f * Acceleration;
            foreach (IForce force in ForceDictionary.Forces)
            {
                for (int i = 0; i < State.Count; i++)
                {
                    for (int j = i + 1; j < State.Count; j++)
                    {
                        Body b1 = State[i];
                        Body b2 = State[j];

                        force.ApplyForce(ref b1, ref b2, deltaT);

                    }
                }
            }

            foreach(Body b in State)
            {
                if (b.Fixed)
                    continue;

                b.Pos += b.Vel * deltaT;

                if (b.Pos.X > SimBounds.MaxX && b.Vel.X > 0)
                    b.Vel.X = 0 - b.Vel.X;
                if (b.Pos.X < SimBounds.MinX && b.Vel.X < 0)
                    b.Vel.X = 0 - b.Vel.X;
                if (b.Pos.Y > SimBounds.MaxY && b.Vel.Y > 0)
                    b.Vel.Y = 0 - b.Vel.Y;
                if (b.Pos.Y < SimBounds.MinY && b.Vel.Y < 0)
                    b.Vel.Y = 0 - b.Vel.Y;
            }
        }
    }
}
