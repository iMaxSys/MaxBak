using System;

namespace iMaxSys.Max.GIS
{
    public class Point
    {
        private double X;
        private double Y;

        public Point()
        {
            X = -1;
            Y = -1;
        }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double Distance(Point p)
        {
            double xdiff = X - p.X;
            double ydiff = Y - p.Y;

            return Math.Sqrt(xdiff * xdiff + ydiff * ydiff);

        }
    }
}
