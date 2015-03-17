using Assets.Scripts.Extensions;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class SplineManager : MonoBehaviour
    {
        #region - Fields -

        public GameObject SplineObject;

        private ISplineSaver _splineSaver;

        private InputField _loadingSplineNameField;

        private StatusBarController _statusBarController;

        #endregion

        #region - Start Inicialization -

        private void Awake()
        {
            _splineSaver = this.GetComponentEx<ISplineSaver>();

            _statusBarController = this.GetComponentEx<StatusBarController>();

            _loadingSplineNameField = this.GetComponentByObjectName<InputField>("LoadSplineName");
        }

        #endregion

        #region - Event Handlers -

        /// <summary>
        /// Обработчик команды создания сплайна
        /// </summary>
        public void OnCreateSpline()
        {
            Instantiate(SplineObject);
        }

        /// <summary>
        /// Обработчик команды удаления сплайна
        /// </summary>
        public void OnDeleteSpline()
        {
            if (SelectionManager.SelectedSpline == null) return;

            DeleteSelectedSpline();

            SelectLastSpline();
        }

        /// <summary>
        /// Выделяет последний созданный сплайн
        /// </summary>
        private void SelectLastSpline()
        {
            if (SplineHolder.Splines.Count > 0)
            {
                var lastSpline = SplineHolder.Splines[SplineHolder.Splines.Count - 1];

                SelectionManager.SelectedSpline = lastSpline;

                lastSpline.SelectFirstKeyPoint();
            }
        }

        /// <summary>
        /// Удаляет выделенный сплайн
        /// </summary>
        private void DeleteSelectedSpline()
        {
            SplineHolder.Splines.Remove(SelectionManager.SelectedSpline);
            Destroy(SelectionManager.SelectedSpline.gameObject);
        }

        /// <summary>
        /// Обработчик команды сохранения сплайна
        /// </summary>
        public void OnSaveSpline()
        {
            if (SelectionManager.SelectedSpline == null) return;

            _splineSaver.SaveSpline(SelectionManager.SelectedSpline);
        }

        /// <summary>
        /// Обработчик команды загрузки сплайна
        /// </summary>
        public void OnLoadSpline()
        {
            if (string.IsNullOrEmpty(_loadingSplineNameField.text))
            {
                _statusBarController.UpdateStatus("Please enter loading spline name");
                return;
            }

            _splineSaver.LoadSpline(_loadingSplineNameField.text);
        }

        #endregion
    }
}
