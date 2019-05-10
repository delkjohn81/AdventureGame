using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    public class Vector
    {
        #region Properties
        protected double x;
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        protected double y;
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public double Magnitude
        {
            get
            {
                return (double)Math.Sqrt(x * x + y * y);
            }
        }

        public Vector Unitized
        {
            get
            {
                return this / Magnitude;
            }
        }

        public double Angle
        {
            get
            {
                if (this.Magnitude == 0)
                {
                    return 0;
                }
                if (y <= 0)
                    return (double)(Math.Asin(X / Magnitude)
                        * 180 / Math.PI);
                else
                    return (double)(180 - Math.Asin(X / Magnitude)
                        * 180 / Math.PI);
            }
        }
        #endregion

        #region Operators
        public static Vector operator + (Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator - (Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }

        public static Vector operator /(Vector v, double f)
        {
            return new Vector(v.X / f, v.Y / f);
        }

        public static Vector operator * (double f, Vector v)
        {
            return new Vector(f * v.X, f * v.Y);
        }

        public static Vector operator * (Vector v, double f)
        {
            return new Vector(f * v.X, f * v.Y);
        }
        #endregion

        #region Constructors
        public Vector()
        {
            x = 0;
            y = 0;
        }
        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector(Vector v)
        {
            x = v.X;
            y = v.Y;
        }

        public Vector(double angle)
        {
            x = (double)Math.Sin(angle*Math.PI/180);
            y = -1*(double)Math.Cos(angle*Math.PI/180);
        }
        #endregion

        #region Methods
        public double AngleBetween(Vector v2)
        {
            double numerator = this.X * v2.X + this.Y * v2.Y;
            double denominator = this.Magnitude * v2.Magnitude;
            return Math.Acos(numerator / denominator);
        }
        #endregion
    }
}
