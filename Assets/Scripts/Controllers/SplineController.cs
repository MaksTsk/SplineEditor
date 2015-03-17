using Assets.Scripts.Extensions;
using Assets.Scripts.Operators;
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
            if (SelectionManager.SelectedSpline != null)
            {
                _maxCurveSlider.value = SelectionManager.SelectedSpline.MaxVerticesCurve;
                _isClosedSplineToggle.isOn = SelectionManager.SelectedSpline.IsClosedSpline;
                _showSourceLineToggle.isOn = SelectionManager.SelectedSpline.DrawSourceLine;
            }
        }

        public void OnMaxCurveValueChanged()
        {
            if (SelectionManager.SelectedSpline != null)
            {
                SelectionManager.SelectedSpline.MaxVerticesCurve = (int)_maxCurveSlider.value;
            }
        }

        public void OnClosedToggleChanged()
        {
            if (SelectionManager.SelectedSpline != null)
            {
                SelectionManager.SelectedSpline.IsClosedSpline = _isClosedSplineToggle.isOn;
            }
        }

        public void OnShowSourceLineToggleChanged()
        {
            if (SelectionManager.SelectedSpline != null)
            {
                SelectionManager.SelectedSpline.DrawSourceLine = _showSourceLineToggle.isOn;
            }
        }
    }
}
