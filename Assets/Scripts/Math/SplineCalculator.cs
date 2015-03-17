using System.Collections.Generic;
using Assets.Scripts.Controllers;
using Assets.Scripts.Entities;
using Assets.Scripts.Operators;
using UnityEngine;

namespace Assets.Scripts.Math
{
    public  class SplineCalculator : MonoBehaviour
    {
        public Point PointPrefab;

        private Spline _spline;

        private void Awake()
        {
            _spline = GetComponent<Spline>();

            if (_spline == null)
            {
                Debug.LogError("Не установлен Spline для SplineCalculator");
            }
        }

        public List<Vector3> GetSplinePoints()
        {
            var listCapacity = (int) System.Math.Pow(_spline.KeyPoints.Count, 2);

            var splinePoints = new List<Vector3>(listCapacity);

            var lineCount = _spline.KeyPoints.Count - (_spline.IsClosedSpline ? 0 : 1);

            for (var i = 0; i < lineCount; i++)
            {
                var j = i - 1;
                if (j < 0)
                {
                    j = _spline.IsClosedSpline ? _spline.KeyPoints.Count - 1 : i;
                }

                var point1 = _spline.KeyPoints[j];
                j = i + 1;
                if (j > _spline.KeyPoints.Count - 1)
                {
                    j = _spline.IsClosedSpline ? 0 : i;
                }
                var point2 = _spline.KeyPoints[j];

                j++;
                if (j > _spline.KeyPoints.Count - 1)
                {
                    j = _spline.IsClosedSpline ? 0 : i;
                }
                var point3 = _spline.KeyPoints[j];

                var tension = _spline.KeyPoints[i].Tension;
                var continuity = _spline.KeyPoints[i].Continuity;
                var bias = _spline.KeyPoints[i].Bias;

                var r1 = 0.5f * (1 - tension) *
                         ((1 + bias) * (1 - continuity) * (_spline.KeyPoints[i].Position - point1.Position) +
                          (1 - bias) * (1 + continuity) * (point2.Position - _spline.KeyPoints[i].Position));

                tension = point2.Tension;
                continuity = point2.Continuity;
                bias = point2.Bias;

                var r2 = 0.5f * (1 - tension) *
                             ((1 + bias) * (1 + continuity) * (point2.Position - _spline.KeyPoints[i].Position) +
                              (1 - bias) * (1 - continuity) * (point3.Position - point2.Position));

                for (var k = 0; k < _spline.MaxVerticesCurve; k++)
                {
                    var t = (float)k / (float)(_spline.MaxVerticesCurve - 1);
                    var v = Interpolate(t, _spline.KeyPoints[i].Position, point2.Position, r1, r2);
                    splinePoints.Add(v);
                }
            }

            return splinePoints;
        }

        private Vector3 Interpolate(float t, Vector3 p1, Vector3 p2, Vector3 r1, Vector3 r2)
        {
            return p1 * (2.0f * t * t * t - 3.0f * t * t + 1.0f) + r1 * (t * t * t - 2.0f * t * t + t) +
                p2 * (-2.0f * t * t * t + 3.0f * t * t) + r2 * (t * t * t - t * t);
        }

        public Point GetPointFromClick()
        {
            var C = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            var minI = 0;
            var minD = Vector3.zero;
            var flag = true;
            var minDistance = float.MaxValue;
            for (var i = 0; i < _spline.KeyPoints.Count - 1; i++)
            {

                var A = CameraController.MainCamera.WorldToScreenPoint(_spline.KeyPoints[i].Position);
                var B = CameraController.MainCamera.WorldToScreenPoint(_spline.KeyPoints[i + 1].Position);

                var D = A + Vector3.Project(C - A, B - A);
                var Va = D - A;
                var Vb = D - B;

                if ((Mathf.Sign(Va.x) != Mathf.Sign(Vb.x) || Va.x == 0 && Vb.x == 0) &&
                    (Mathf.Sign(Va.y) != Mathf.Sign(Vb.y) || Va.y == 0 && Vb.y == 0) &&
                    (Mathf.Sign(Va.z) != Mathf.Sign(Vb.z) || Va.z == 0 && Vb.z == 0) &&
                    Vector3.Distance(D, C) < minDistance)
                {
                    minI = i;
                    minD = D;
                    minDistance = Vector3.Distance(D, C);
                    flag = false;
                }
            }

            if (_spline.IsClosedSpline)
            {
                var A = CameraController.MainCamera.WorldToScreenPoint(_spline.KeyPoints[0].Position);
                var B =
                    CameraController.MainCamera.WorldToScreenPoint(_spline.KeyPoints[_spline.KeyPoints.Count - 1].Position);

                var D = A + Vector3.Project(C - A, B - A);
                var Va = D - A;
                var Vb = D - B;

                if ((Mathf.Sign(Va.x) != Mathf.Sign(Vb.x) || Va.x == 0 && Vb.x == 0) &&
                    (Mathf.Sign(Va.y) != Mathf.Sign(Vb.y) || Va.y == 0 && Vb.y == 0) &&
                    (Mathf.Sign(Va.z) != Mathf.Sign(Vb.z) || Va.z == 0 && Vb.z == 0) &&
                    Vector3.Distance(D, C) < minDistance)
                {
                    minI = _spline.KeyPoints.Count - 1;
                    minD = D;
                    minDistance = Vector3.Distance(D, C);
                    flag = false;
                }
            }

            if (flag)
            {
                return null;
            }

            var point = Instantiate(PointPrefab);
            point.transform.parent = transform;
            var curentPos = CameraController.MainCamera.ScreenToWorldPoint(minD);
            point.transform.position = curentPos;

            return point;
        }
    }
}
