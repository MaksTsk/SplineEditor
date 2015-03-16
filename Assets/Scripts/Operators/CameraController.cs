using UnityEngine;

namespace Assets.Scripts.Operators
{
    public class CameraController : MonoBehaviour
    {

        // Use this for initialization
        private void Awake()
        {
            MainCamera = GetComponent<Camera>();
        }

        // Update is called once per frame
        private void Update()
        {

        }

        public static Camera MainCamera { get; private set; }
    }
}
