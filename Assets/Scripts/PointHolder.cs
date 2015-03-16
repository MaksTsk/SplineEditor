using UnityEngine;

namespace Assets.Scripts
{
    public class PointHolder : MonoBehaviour {

        public GameObject PointObject;

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        public void CreatePoint()
        {
            Instantiate(PointObject, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
