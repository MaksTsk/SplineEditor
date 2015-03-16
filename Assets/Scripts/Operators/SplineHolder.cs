using System.Collections.Generic;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Operators
{
    public class SplineHolder : MonoBehaviour
    {
        public static List<Spline> Splines
        {
            get
            {
                if (_splines == null)
                {
                    _splines = new List<Spline>();
                }
                return _splines;
            }

            set { _splines = value; }
        }

        public GameObject SplineObject;

        private static List<Spline> _splines;

        // Use this for initialization
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {

        }

        public void CreateSpline()
        {
            Instantiate(SplineObject, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
