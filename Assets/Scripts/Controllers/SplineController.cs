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

        private InputField _splineNameInput;

        // Use this for initialization
        private void Start()
        {
            _maxCurveSlider = GameObjectExtension.GetComponentByObjectName<Slider>("MaxCurveSlider");

            _isClosedSplineToggle = GameObjectExtension.GetComponentByObjectName<Toggle>("IsCloseSplineToggle");

            _showSourceLineToggle = GameObjectExtension.GetComponentByObjectName<Toggle>("ShowSourceLineToggle");

            _splineNameInput = GameObjectExtension.GetComponentByObjectName<InputField>("SplineNameInput");
        }


        // Update is called once per frame
        private void Update()
        {
            if (SelectionManager.SelectedSpline != null)
            {
                _maxCurveSlider.value = SelectionManager.SelectedSpline.MaxVerticesCurve;
                _isClosedSplineToggle.isOn = SelectionManager.SelectedSpline.IsClosedSpline;
                _showSourceLineToggle.isOn = SelectionManager.SelectedSpline.DrawSourceLine;
                _splineNameInput.text = SelectionManager.SelectedSpline.name;
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

        public void OnSplineNameChanged()
        {
            if (SelectionManager.SelectedSpline != null)
            {
                SelectionManager.SelectedSpline.name = _splineNameInput.text;
            }
        }

        public void OnDeletePoint()
        {
            var spline = SelectionManager.SelectedSpline;

            var point = SelectionManager.SelectedPoint;

            if (spline != null && point != null && spline.KeyPoints.Count > 3)
            {
                spline.KeyPoints.Remove(point);
                Destroy(point.gameObject);

                spline.SelectFirstKeyPoint();
            }
        }
    }
}
