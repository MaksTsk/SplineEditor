using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Spline : MonoBehaviour, ISelectableObject
    {
        public List<Point> SplinePoints;

        // Use this for initialization
        private void Start()
        {
        }


        // Update is called once per frame
        private void Update()
        {

        }

        public void Select()
        {
            Debug.Log("Spline select");

            PointHolder.SelectedSpline = this;
        }

        public bool IsSelected { get { return ReferenceEquals(PointHolder.SelectedSpline, this); } }
    }
}
