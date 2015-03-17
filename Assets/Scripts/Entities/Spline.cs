using System.Collections.Generic;
using Assets.Scripts.Controllers;
using Assets.Scripts.Extensions;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Math;
using Assets.Scripts.Operators;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Spline : MonoBehaviour, ISelectableObject
    {
        public const int DefaultPointsCount = 3;

        public List<Point> KeyPoints;

        public bool IsClosedSpline = true;

        public int MaxVerticesCurve = 50;

        public bool DrawSourceLine = true;

        public GameObject PointPrefab;
        public List<Vector3> SplinePoints { get; private set; }

        private ISplineCalculator _splineCalculator;

        private void Awake()
        {
            _splineCalculator = this.GetComponentEx<ISplineCalculator>();
        }

        // Use this for initialization
        private void Start()
        {
            SelectionManager.SelectedSpline = this;

            SelectFirstKeyPoint();

            SplineHolder.Splines.Add(this);

            SplinePoints = new List<Vector3>();
        }

        public void SelectFirstKeyPoint()
        {
            if (KeyPoints.Count > 0)
            {
                SelectionManager.SelectedPoint = KeyPoints[0];
            }
        }

        // Update is called once per frame
        private void Update()
        {
            SplinePoints = _splineCalculator.GetSplinePoints();

            if (IsSelected && Input.GetMouseButtonDown(1))
            {
                var pointLocation = _splineCalculator.GetPointLocationFromClick();

                if (pointLocation != Vector3.zero)
                {
                    CreatePointByLocation(pointLocation);
                }
            }
        }

        private void CreatePointByLocation(Vector3 pointLocation)
        {
            var point = Instantiate(PointPrefab);
            point.transform.parent = transform;
            var curentPos = CameraController.MainCamera.ScreenToWorldPoint(pointLocation);
            point.transform.position = curentPos;
            KeyPoints.Add(point.GetComponent<Point>());
        }

        public void Select()
        {
            SelectionManager.SelectedSpline = this;
        }

        public bool IsSelected { get { return ReferenceEquals(SelectionManager.SelectedSpline, this); } }

    }
}
