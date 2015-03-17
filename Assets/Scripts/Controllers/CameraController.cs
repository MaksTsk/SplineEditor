using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class CameraController : MonoBehaviour
    {

        // Use this for initialization
        private void Awake()
        {
            MainCamera = this.GetComponentEx<Camera>();
        }

        public static Camera MainCamera { get; private set; }
    }
}
