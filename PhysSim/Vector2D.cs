using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysSim
{
    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        double _magnitude = -1;
        public double Magnitude
        {
            get
            {
                if (_magnitude == -1)
                {
                    _magnitude = Math.Sqrt(X * X + Y * Y);
                }
                return _magnitude;
            }
        }

        public Vector2D Inverse
        {
            get
            {
                return new Vector2D() { X = -X, Y = -Y };
            }
        }

        public Vector2D UnitVector
        {
            get
            {
                return new Vector2D() { X = X/(Magnitude), Y = Y/(Magnitude) };
            }
        }

        public static Vector2D Random(double scale)
        {
            throw new NotImplementedException();
        }

        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            return new Vector2D() { X = v1.X + v2.X, Y = v1.Y + v2.Y };
        }

        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return new Vector2D() { X = v1.X - v2.X, Y = v1.Y - v2.Y };
        }

        public static Vector2D operator *(Vector2D v1, double s)
        {
            return new Vector2D() { X = v1.X * s, Y = v1.Y * s };
        }


        public static Vector2D operator *(double s, Vector2D v1)
        {
            return new Vector2D() { X = v1.X * s, Y = v1.Y * s };
        }

        public static Vector2D operator /(Vector2D v1, double s)
        {
            return new Vector2D() { X = v1.X / s, Y = v1.Y / s };
        }

        public static double operator *(Vector2D v1, Vector2D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }
    }
}
