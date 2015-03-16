using System.Collections.Generic;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Math
{
    public static class SplineCalculator
    {
        public static List<Vector3> GetSplinePoints(List<Point> keyPoints, bool isClosedSpline, int maxCurve)
        {
            var listCapacity = (int) System.Math.Pow(keyPoints.Count, 2);

            var splinePoints = new List<Vector3>(listCapacity);

            var lineCount = keyPoints.Count - (isClosedSpline ? 0 : 1);

            for (var i = 0; i < lineCount; i++)
            {
                var j = i - 1;
                if (j < 0)
                {
                    j = isClosedSpline ? keyPoints.Count - 1 : i;
                }

                var point1 = keyPoints[j];
                j = i + 1;
                if (j > keyPoints.Count - 1)
                {
                    j = isClosedSpline ? 0 : i;
                }
                var point2 = keyPoints[j];

                j++;
                if (j > keyPoints.Count - 1)
                {
                    j = isClosedSpline ? 0 : i;
                }
                var point3 = keyPoints[j];

                var tension = keyPoints[i].Tension;
                var continuity = keyPoints[i].Continuity;
                var bias = keyPoints[i].Bias;

                var r1 = 0.5f * (1 - tension) *
                         ((1 + bias) * (1 - continuity) * (keyPoints[i].Position - point1.Position) +
                          (1 - bias) * (1 + continuity) * (point2.Position - keyPoints[i].Position));

                tension = point2.Tension;
                continuity = point2.Continuity;
                bias = point2.Bias;

                var r2 = 0.5f * (1 - tension) *
                             ((1 + bias) * (1 + continuity) * (point2.Position - keyPoints[i].Position) +
                              (1 - bias) * (1 - continuity) * (point3.Position - point2.Position));

                for (var k = 0; k < maxCurve; k++)
                {
                    var t = (float)k / (float)(maxCurve - 1);
                    var v = Interpolate(t, keyPoints[i].Position, point2.Position, r1, r2);
                    splinePoints.Add(v);
                }
            }

            return splinePoints;
        }

        private static Vector3 Interpolate(float t, Vector3 p1, Vector3 p2, Vector3 r1, Vector3 r2)
        {
            return p1 * (2.0f * t * t * t - 3.0f * t * t + 1.0f) + r1 * (t * t * t - 2.0f * t * t + t) +
                p2 * (-2.0f * t * t * t + 3.0f * t * t) + r2 * (t * t * t - t * t);
        }
    }
}
