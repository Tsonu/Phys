using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysSim
{
    public class Body
    {
        public double Mass { get; set; }
        public double Radius { get; set; }
        public double Charge { get; set; }

        public Vector2D Pos { get; set; }
        public Vector2D Vel { get; set; }

        public bool Fixed { get; set; }

        public SimState State { get; set; }

        public Body()
        {

        }
        public Body(double init)
        {
            Mass = init * 10e3;
            Radius = init;
            Charge = init;
            Pos = new Vector2D() { X = init, Y = init };
            Vel = new Vector2D() { X = init, Y = init };
        }

        public Vector2D DirectionTo(Body other)
        {
            return Pos - other.Pos;
        }

        public double DistanceTo(Body other)
        {
            return DirectionTo(other).Magnitude;
        }

        public static Body Earth
        {
            get
            {
                return new Body() { Mass = 5.972e24, Radius = 6.371e6, Charge = 0,
                                    Pos = new Vector2D() {X = 0, Y= 0},
                                    Vel = new Vector2D() {X = 0, Y= 0},
                                    Fixed = false };
            }
        }

        public static Body Moon
        {
            get
            {
                return new Body()
                {
                    Mass = 7.3477e22,
                    Radius = 1.737e6,
                    Charge = 0,
                    Pos = new Vector2D() { X = 3.84399e8, Y = 0 },
                    Vel = new Vector2D() { X = 0, Y = 1.022e3 },
                    Fixed = false
                };
            }
        }


        public static Body BlackHole
        {
            get
            {
                return new Body()
                {
                    Mass = 7.3477e27,
                    Radius = 2.737e7,
                    Charge = 0,
                    Pos = new Vector2D() { X = 0, Y = 0 },
                    Vel = new Vector2D() { X = 0, Y = 0 },
                    Fixed = true
                };
            }
        }


        internal void Delete()
        {
            State.Remove(this);
        }
    }
}
