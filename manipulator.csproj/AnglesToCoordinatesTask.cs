using System;
using System.Drawing;
using NUnit.Framework;
 
namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {        
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var constanta = 2;
            var positionX = Manipulator.UpperArm * (float)Math.Cos(shoulder);
            var positionY = Manipulator.UpperArm * (float)Math.Sin(shoulder);
            var elbowPosition = new PointF(positionX, positionY);
 
            positionX += Manipulator.Forearm * (float)Math.Cos(elbow + shoulder - Math.PI);
            positionY += Manipulator.Forearm * (float)Math.Sin(elbow + shoulder - Math.PI);
            var wristPosition = new PointF(positionX, positionY);
 
            positionX += Manipulator.Palm * (float)Math.Cos(wrist + elbow + shoulder - constanta * Math.PI);
            positionY += Manipulator.Palm * (float)Math.Sin(wrist + elbow + shoulder - constanta * Math.PI);
            var palmEndPosition = new PointF(positionX, positionY);

            return new PointF[]
            {
                elbowPosition,
                wristPosition,
                palmEndPosition
            };
        }
    }
    [TestFixture]
    public class AnglesToCoordinatesTaskTests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI / 3, Math.PI, 155.88456726074219, 60)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI/ 2.120, 90)]
        [TestCase(0D, 0D, 0D, 0D, 0D)]
        [TestCase(0D, 0D, 0D, 0D, 0D)]
        [TestCase(Math.PI / 8, Math.PI / 9, Math.PI/2, 9.57323, 20.56833)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
        }
    }
}