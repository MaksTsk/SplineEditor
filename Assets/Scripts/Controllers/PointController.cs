using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    /// <summary>
    /// Компонент управления точкой.
    /// </summary>
    public class PointController : MonoBehaviour
    {
        #region - Fields -

        private Slider _tensionSlider;

        private Slider _biasSlider;

        private Slider _continuitySlider;

        #endregion

        #region - Event Handlers -

        // Use this for initialization
        private void Start()
        {
            _tensionSlider = this.GetComponentByObjectName<Slider>("TensionSlider");

            _biasSlider = this.GetComponentByObjectName<Slider>("BiasSlider");

            _continuitySlider = this.GetComponentByObjectName<Slider>("ContinuitySlider");
        }

        // Update is called once per frame
        private void Update()
        {
            if (SelectionManager.SelectedPoint != null)
            {
                _tensionSlider.value = SelectionManager.SelectedPoint.Tension;

                _biasSlider.value = SelectionManager.SelectedPoint.Bias;

                _continuitySlider.value = SelectionManager.SelectedPoint.Continuity;
            }
        }

        /// <summary>
        /// Обработчик события изменения Tension
        /// </summary>
        public void OnTensionChanged()
        {
            if (SelectionManager.SelectedPoint != null)
            {
                SelectionManager.SelectedPoint.Tension = _tensionSlider.value;
            }
        }

        /// <summary>
        /// Обработчик события изменения Bias
        /// </summary>
        public void OnBiasChanged()
        {
            if (SelectionManager.SelectedPoint != null)
            {
                SelectionManager.SelectedPoint.Bias = _biasSlider.value;
            }
        }

        /// <summary>
        /// Обработчик события изменения Continuity
        /// </summary>
        public void OnContinuityChanged()
        {
            if (SelectionManager.SelectedPoint != null)
            {
                SelectionManager.SelectedPoint.Continuity = _continuitySlider.value;
            }
        }

        /// <summary>
        /// Обработчик вызова сброса параметров точки
        /// </summary>
        public void OnResetClicked()
        {
            if (SelectionManager.SelectedPoint != null)
            {
                SelectionManager.SelectedPoint.RestoreDefaultProperties();
            }
        }

        #endregion
    }
}
