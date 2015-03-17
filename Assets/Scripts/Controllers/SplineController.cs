using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    /// <summary>
    /// Компонент управления сплайном
    /// </summary>
    public class SplineController : MonoBehaviour
    {
        #region - Fields -

        private Slider _maxCurveSlider;

        private Toggle _isClosedSplineToggle;

        private Toggle _showSourceLineToggle;

        private InputField _splineNameInput;

        #endregion

        #region - Event Handlers -

        // Use this for initialization
        private void Start()
        {
            _maxCurveSlider = this.GetComponentByObjectName<Slider>("MaxCurveSlider");

            _isClosedSplineToggle = this.GetComponentByObjectName<Toggle>("IsCloseSplineToggle");

            _showSourceLineToggle = this.GetComponentByObjectName<Toggle>("ShowSourceLineToggle");

            _splineNameInput = this.GetComponentByObjectName<InputField>("SplineNameInput");
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
                SelectionManager.SelectedSpline.MaxVerticesCurve = (int) _maxCurveSlider.value;
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

        #endregion
    }
}
