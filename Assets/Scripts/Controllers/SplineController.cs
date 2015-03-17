using Assets.Scripts.Extensions;
using Assets.Scripts.Operators;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class SplineController : MonoBehaviour
    {

        private Slider _maxCurveSlider;

        private Toggle _isClosedSplineToggle;

        private Toggle _showSourceLineToggle;

        // Use this for initialization
        private void Start()
        {
            _maxCurveSlider = GameObjectExtension.GetComponentByObjectName<Slider>("MaxCurveSlider");

            _isClosedSplineToggle = GameObjectExtension.GetComponentByObjectName<Toggle>("IsCloseSplineToggle");

            _showSourceLineToggle = GameObjectExtension.GetComponentByObjectName<Toggle>("ShowSourceLineToggle");
        }


        // Update is called once per frame
        private void Update()
        {
            if (Selector.SelectedSpline != null)
            {
                _maxCurveSlider.value = Selector.SelectedSpline.MaxVerticesCurve;
                _isClosedSplineToggle.isOn = Selector.SelectedSpline.IsClosedSpline;
                _showSourceLineToggle.isOn = Selector.SelectedSpline.DrawSourceLine;
            }
        }

        public void OnMaxCurveValueChanged()
        {
            if (Selector.SelectedSpline != null)
            {
                Selector.SelectedSpline.MaxVerticesCurve = (int)_maxCurveSlider.value;
            }
        }

        public void OnClosedToggleChanged()
        {
            if (Selector.SelectedSpline != null)
            {
                Selector.SelectedSpline.IsClosedSpline = _isClosedSplineToggle.isOn;
            }
        }

        public void OnShowSourceLineToggleChanged()
        {
            if (Selector.SelectedSpline != null)
            {
                Selector.SelectedSpline.DrawSourceLine = _showSourceLineToggle.isOn;
            }
        }
    }
}
