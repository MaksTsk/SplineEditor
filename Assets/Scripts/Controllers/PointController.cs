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
            if (Selector.SelectedPoint != null)
            {
                _tensionSlider.value = Selector.SelectedPoint.Tension;

                _biasSlider.value = Selector.SelectedPoint.Bias;

                _continuitySlider.value = Selector.SelectedPoint.Continuity;
            }
        }

        public void OnTensionChanged()
        {
            if (Selector.SelectedPoint != null)
            {
                Selector.SelectedPoint.Tension = _tensionSlider.value;
            }
        }

        public void OnBiasChanged()
        {
            if (Selector.SelectedPoint != null)
            {
                Selector.SelectedPoint.Bias = _biasSlider.value;
            }
        }

        public void OnContinuityChanged()
        {
            if (Selector.SelectedPoint != null)
            {
                Selector.SelectedPoint.Continuity = _continuitySlider.value;
            }
        }
    }
}
