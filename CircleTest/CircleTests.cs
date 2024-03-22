using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Circle;
using Point = Circle.Helper.Point;
using System.Runtime.Remoting.Messaging;
using System.Linq;

namespace CircleTest
{
    [TestClass]
    public class CircleTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
            "The second point out of range of the circle")]
        public void TestException()
        {
            Helper.Calc(20f, 0f,
                        40f, 50f,
                        15, 90,
                        Helper.Vector.Сounterclockwise, 5);
        }

        [TestMethod]
        public void ManualСounterclockwiseTest()
        {
            Array expected = new Point[]
            {
                new Point() {X = 25, Y = 39, Angle = 75},
                new Point() {X = 27, Y = 1, Angle = 291},
                new Point() {X = 39, Y = 21, Angle = 363},
                new Point() {X = 4, Y = 7, Angle = 220},
            };
            Point[] points = Helper.Calc(25.18f, 4.7f,
                39.32f, 7.13f,
                20, 75,
                Helper.Vector.Сounterclockwise, 2);
            for (int i = 0; i < points.Length; i++)
            {
                points[i].X = (float)Math.Floor(points[i].X);
                points[i].Y = (float)Math.Floor(points[i].Y);
                points[i].Angle = (float)Math.Floor(points[i].Angle);
            }
            CollectionAssert.AreEqual(expected, points);
        }

        [TestMethod]
        public void ManualClockwiseTest()
        {
            Array expected = new Point[]
            {
                new Point() {X = 25, Y = 39, Angle = 75},
                new Point() {X = 0, Y = 22, Angle = 171},
                new Point() {X = 9, Y = 36, Angle = 123},
                new Point() {X = 4, Y = 7, Angle = 220},
            };
            Point[] points = Helper.Calc(25.18f, 4.7f,
                39.32f, 7.13f,
                20, 75,
                Helper.Vector.Сlockwise, 2);
            for (int i = 0; i < points.Length; i++)
            {
                points[i].X = (float)Math.Floor(points[i].X);
                points[i].Y = (float)Math.Floor(points[i].Y);
                points[i].Angle = (float)Math.Floor(points[i].Angle);
            }
            CollectionAssert.AreEqual(expected, points);
        }
    }
}
