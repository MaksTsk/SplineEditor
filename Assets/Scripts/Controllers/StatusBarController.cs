using System.Threading;
using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class StatusBarController : MonoBehaviour
    {
        public Text StatusInput;

        private Timer _timer;

        // Use this for initialization
        private void Start()
        {
            UpdateStatus(string.Empty);
        }

        // Update is called once per frame
        private void Update()
        {
            StatusInput.text = Status;
        }

        public void UpdateStatus(string status)
        {
            Status = status;
        }
        private string Status { get; set; }
    }
}
