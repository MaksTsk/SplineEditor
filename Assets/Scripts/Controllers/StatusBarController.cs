using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class StatusBarController : MonoBehaviour
    {
        public Text StatusInput;

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

        /// <summary>
        /// Статус
        /// </summary>
        private string Status { get; set; }

        /// <summary>
        /// Обновляет статус бар
        /// </summary>
        /// <param name="status">статус</param>
        public void UpdateStatus(string status)
        {
            Status = status;
        }
    }
}
