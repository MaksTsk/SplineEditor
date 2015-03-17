using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class StatusBarController : MonoBehaviour
    {
        public static string Status = string.Empty;

        private Text _statusInput;

        // Use this for initialization
        private void Start()
        {
            _statusInput = GameObjectExtension.GetComponentByObjectName<Text>("StatusBar");
        }

        // Update is called once per frame
        private void Update()
        {
            _statusInput.text = Status;
        }
    }
}
