using System.Collections.Generic;
using UnityEngine;

namespace Utomo
{
    public static class Bezier
    {

        private static int numpoints = 20;

        /// <summary>
        /// To get the transform movement like curve with Bezier Quadratic
        /// </summary>
        /// <param name="points"> The list you want to save transform of curve </param>
        /// <param name="startPoint"> Start position you want to move </param>
        /// <param name="destinationPoint"> The destination of your movement </param>
        /// <param name="controlPos"> Transform control to control the curve shape </param>
        public static void BezierCurve(ref List<Vector2> points, Vector2 startPoint, Vector2 destinationPoint,
            Vector2 controlPos)
        {
            float t = 0;
            points.Clear();
            //List<Vector2> points = new List<Vector2>();
            for (int i = 1; i < numpoints + 1; i++)
            {
                t = i / (float) numpoints;
                points.Add(GetBezierCurves(startPoint, destinationPoint, controlPos, t));
            }
        }

        private static Vector2 GetBezierCurves(Vector2 startPoint, Vector2 destinationPoint, Vector2 tempPos, float t)
        {
            return ((1 - t) * ((1 - t) * startPoint + t * tempPos)) +
                   (t * ((1 - t) * tempPos + (t * destinationPoint)));
        }

        /// <summary>
        /// To set the numpoints. The default is 10
        /// </summary>
        /// <param name="_numpoints"></param>
        public static void SetNumPoints(int _numpoints)
        {
            numpoints = _numpoints;
        }
        
        public static int GetNumPoints()
        {
            return numpoints;
        }
    }

    public static class UtoMath
    {
        public static int GetSign(float number)
        {
            return (int) (number / Mathf.Abs(number));
        }
        
        public static int GetRadiantSection(Vector2 point)
        {
            if (point.x >= 0 && point.y >= 0) return 1;
            else if (point.x < 0 && point.y >= 0) return 2;
            else if (point.x < 0 && point.y < 0) return 3;
            else return 4;
        }

        public static int CheckRadiantGroup(Vector2 a, Vector2 b)
        {
            int radiantA = GetRadiantSection(a);
            int radiantB = GetRadiantSection(b);

            if (radiantA == radiantB) return -1;
            else if (radiantA == 1 && radiantB == 3) return -1;
            else if (radiantA == 2 && radiantB == 4) return 1;
            else if (radiantA == 3 && radiantB == 1) return -1;
            else if (radiantA == 4 && radiantB == 2) return 1;
            else return 0;
        }

        public static float GetDegreeFromRadiantSection(Vector2 a, Vector2 b)
        {
            int radiantA = GetRadiantSection(a);
            int radiantB = GetRadiantSection(b);

            if (radiantA == 1 && radiantB == 1) return 90;
            else if (radiantA == 1 && radiantB == 3) return -90;
            else if (radiantA == 1 && radiantB == 4) return 0;
            else if (radiantA == 2 && radiantB == 2) return 90;
            else if (radiantA == 2 && radiantB == 4) return -90;
            else if (radiantA == 2 && radiantB == 1) return 0;
            else if (radiantA == 3 && radiantB == 1) return -90;
            else if (radiantA == 3 && radiantB == 2) return 0;
            else if (radiantA == 3 && radiantB == 3) return 90;
            else if (radiantA == 4 && radiantB == 2) return -90;
            else if (radiantA == 4 && radiantB == 3) return 0;
            else return 90;
        }
    }
}