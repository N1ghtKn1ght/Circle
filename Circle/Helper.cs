using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle
{
    public static class Helper
    {
        public static Point[] Calc(float x1, float x2, 
                                    float y1, float y2, 
                                    float radius, float angle, 
                                    Vector vector, int count)
        {
            Point p1 = new Point() { X = x1, Y = y1, Angle = angle };
            Point p0 = FindPoint(p1, -radius, angle);
            Point d0 = new Point() { X = p0.X + radius, Y = p0.Y, Angle = 0};
            Point p2 = new Point() { X = x2, Y = y2, Angle = 0};
            p2.Angle = Helper.AngleBetween(p0, p2, d0);
            if(!CheckPoint(p0, radius, ref p2))
                throw new ArgumentOutOfRangeException("The second point out of range of the circle");
            float betweenAngles = Helper.AngleBetween(p0, p2, p1) % 180;
            float step = ((360 * (int)vector) - betweenAngles) / (count + 1);
            float d = p2.Angle;
            Point[] points = new Point[count + 2];
            for (int i = 1; i < count + 1; i++)
            {
                d += step;
                Point point = FindPoint(p0, radius, d);
                points[i].X = point.X;
                points[i].Y = point.Y;
                points[i].Angle = d;
            }
            points[0] = p1;
            points[points.Length - 1] = p2;
            return points;
        }

        private static bool CheckPoint(Point zeroPosition, float radius, ref Point point)
        {
            float rad = Deg2Rad(point.Angle);
            float x = zeroPosition.X + (radius * (float)Math.Cos(rad));
            float y = zeroPosition.Y + (radius * (float)Math.Sin(rad));
            if(x - 5 < point.X && x + 5 > point.X 
                && y - 5 < point.Y && y + 5 > point.Y)
            {
                point.X = x; 
                point.Y = y;
                return true;
            }    
            return false;
        }

        public static Point FindPoint(Point p, float radius, float angle)
        {
            float rad = Deg2Rad(angle);
            float x = p.X + (radius * (float)Math.Cos(rad));
            float y = p.Y + (radius * (float)Math.Sin(rad));
            return new Point() { X = x, Y = y };
        }

        /*
         *    p
         *   /
         *  /
         * p0 - - d0
         * 
         */
        private static float AngleBetween(Point p0, Point p, Point d0) 
        {
            float radians = (float)Math.Atan2(d0.Y - p0.Y, d0.X - p0.X) - (float)Math.Atan2(p.Y - p0.Y, p.X - p0.X);
            float degrees = (float)Math.Round((180 / (float)Math.PI) * radians);
            float e = 360 * (p.Y < 0 ? 1 : 0);
            return Math.Abs(degrees - e);
        }

        private static float Deg2Rad(float degrees) => (float)Math.PI * degrees / 180.0f;

        public struct Point
        {
            public float X, Y;
            public float Angle;

        }

        public enum Vector
        {
            Сlockwise = 0,
            Сounterclockwise = 1,
        }

    }
}
