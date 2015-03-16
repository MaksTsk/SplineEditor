using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts
{
    public class PointHolder : MonoBehaviour {

        public GameObject SplineObject;

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        public void CreateSpline()
        {
            Instantiate(SplineObject, new Vector3(0, 0, 0), Quaternion.identity);
        }

        public static Point SelectedPoint { get; set; }

        public static Spline SelectedSpline { get; set; }
    }
}
