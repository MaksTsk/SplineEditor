using Assets.Scripts.Extensions;
using Assets.Scripts.Operators;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class PointController : MonoBehaviour
    {
        private Slider _tensionSlider;

        private Slider _biasSlider;

        private Slider _continuitySlider;


        // Use this for initialization
        private void Start()
        {
            _tensionSlider = GameObjectExtension.GetComponentByObjectName<Slider>("TensionSlider");

            _biasSlider = GameObjectExtension.GetComponentByObjectName<Slider>("BiasSlider");

            _continuitySlider = GameObjectExtension.GetComponentByObjectName<Slider>("ContinuitySlider");
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

        public void OnTensionChanged()
        {
            if (SelectionManager.SelectedPoint != null)
            {
                SelectionManager.SelectedPoint.Tension = _tensionSlider.value;
            }
        }

        public void OnBiasChanged()
        {
            if (SelectionManager.SelectedPoint != null)
            {
                SelectionManager.SelectedPoint.Bias = _biasSlider.value;
            }
        }

        public void OnContinuityChanged()
        {
            if (SelectionManager.SelectedPoint != null)
            {
                SelectionManager.SelectedPoint.Continuity = _continuitySlider.value;
            }
        }

        public void OnResetClicked()
        {
            if (SelectionManager.SelectedPoint != null)
            {
                SelectionManager.SelectedPoint.RestoreDefaultProperties();
            }
        }
    }
}
