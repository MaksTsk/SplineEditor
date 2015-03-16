using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Math;
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

        private SplineCalculator _splineCalculator;

        private void Awake()
        {
            _splineCalculator = GetComponent<SplineCalculator>();
        }

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
            SplinePoints = _splineCalculator.GetSplinePoints();

            if (IsSelected && Input.GetMouseButtonDown(1))
            {
                var point = _splineCalculator.GetPointFromClick();

                if (point != null)
                {
                    KeyPoints.Add(point);
                }
            }
        }

        public void Select()
        {
            Selector.SelectedSpline = this;
        }

        public bool IsSelected { get { return ReferenceEquals(Selector.SelectedSpline, this); } }

    }
}
