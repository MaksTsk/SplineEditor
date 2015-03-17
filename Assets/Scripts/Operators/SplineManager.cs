using System;
using Assets.Scripts.Controllers;
using Assets.Scripts.Entities;
using Assets.Scripts.Extensions;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Operators
{
    public class SplineManager : MonoBehaviour
    {
        public GameObject SplineObject;

        private ISplineSaver _splineSaver;

        private InputField _loadingSplineNameField;

        private StatusBarController _statusBarController;
        private void Awake()
        {
            _splineSaver = this.GetComponentEx<ISplineSaver>();

            _statusBarController = this.GetComponentEx<StatusBarController>();

            _loadingSplineNameField = this.GetComponentByObjectName<InputField>("LoadSplineName");
        }

        public void OnCreateSpline()
        {
            Instantiate(SplineObject);
        }

        public void OnDeleteSpline()
        {
            if (SelectionManager.SelectedSpline == null) return;

            var deletedSplineName = SelectionManager.SelectedSpline.name;

            DeleteSelectedSpline();

            SelectRemainSpline();

            _statusBarController.UpdateStatus(string.Format("Spline with name \"{0}\" was succesfully deleted",deletedSplineName));
        }

        private void SelectRemainSpline()
        {
            if (SplineHolder.Splines.Count > 0)
            {
                var firstSpline = SplineHolder.Splines[0];

                SelectionManager.SelectedSpline = firstSpline;

                firstSpline.SelectFirstKeyPoint();
            }
        }

        private void DeleteSelectedSpline()
        {
            SplineHolder.Splines.Remove(SelectionManager.SelectedSpline);
            Destroy(SelectionManager.SelectedSpline.gameObject);
        }

        public void OnSaveSpline()
        {
            if (SelectionManager.SelectedSpline == null) return;

            _splineSaver.SaveSpline(SelectionManager.SelectedSpline);
        }

        public void OnLoadSpline()
        {
            if (SelectionManager.SelectedSpline == null) return;

            if (string.IsNullOrEmpty(_loadingSplineNameField.text))
            {
                _statusBarController.UpdateStatus("Please enter loading spline name");
                return;
            }

            _splineSaver.LoadSpline(_loadingSplineNameField.text);
        }
    }
}
