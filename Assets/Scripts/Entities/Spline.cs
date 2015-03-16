using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Operators;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Spline : MonoBehaviour, ISelectableObject
    {
        public List<Point> KeyPoints;

        public bool IsClosedSpline;

        public int MaxVerticesCurve =10;

        public bool DrawSourceLine;

        public List<Vector3> SplinePoints { get; private set; }

        // Use this for initialization
        private void Start()
        {
            Selector.SelectedSpline = this;
            SplineHolder.Splines.Add(this);

            SplinePoints = new List<Vector3>();
        }


        // Update is called once per frame
        private void Update()
        {
            UpdateSplinePoints();
        }

        public void Select()
        {
            Selector.SelectedSpline = this;
        }

        public bool IsSelected { get { return ReferenceEquals(Selector.SelectedSpline, this); } }

        private void UpdateSplinePoints()
        {
            SplinePoints.Clear();

            var lineCount = KeyPoints.Count - (IsClosedSpline ? 0 : 1);

            for (var i = 0; i < lineCount; i++)
            {
                var j = i - 1;
                if (j < 0)
                {
                    j = IsClosedSpline ? KeyPoints.Count - 1 : i;
                }

                var point1 = KeyPoints[j];
                j = i + 1;
                if (j > KeyPoints.Count - 1)
                {
                    j = IsClosedSpline ? 0 : i;
                }
                var point2 = KeyPoints[j];

                j++;
                if (j > KeyPoints.Count - 1)
                {
                    j = IsClosedSpline ? 0 : i;
                }
                var point3 = KeyPoints[j];

                var tension = KeyPoints[i].Tension;
                var continuity = KeyPoints[i].Continuity;
                var bias = KeyPoints[i].Bias;

                var r1 = 0.5f*(1 - tension)*
                         ((1 + bias)*(1 - continuity)*(KeyPoints[i].Position - point1.Position) +
                          (1 - bias)*(1 + continuity)*(point2.Position - KeyPoints[i].Position));

                tension = point2.Tension;
                continuity = point2.Continuity;
                bias = point2.Bias;

                var r2 = 0.5f*(1 - tension)*
                             ((1 + bias) * (1 + continuity) * (point2.Position - KeyPoints[i].Position) +
                              (1 - bias) * (1 - continuity) * (point3.Position - point2.Position));

                for (var k = 0; k < MaxVerticesCurve; k++)
                {
                    var t = (float)k / (float)(MaxVerticesCurve - 1);
                    var v = Interpolate(t, KeyPoints[i].Position, point2.Position, r1, r2);
                    SplinePoints.Add(v);
                }
            }
        }

        private Vector3 Interpolate(float t, Vector3 p1, Vector3 p2, Vector3 r1, Vector3 r2)
        {
            return p1 * (2.0f * t * t * t - 3.0f * t * t + 1.0f) + r1 * (t * t * t - 2.0f * t * t + t) +
                p2 * (-2.0f * t * t * t + 3.0f * t * t) + r2 * (t * t * t - t * t);
        }
    }
}
