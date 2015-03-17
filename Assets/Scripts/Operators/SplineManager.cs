﻿using Assets.Scripts.Entities;
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

        private InputField _splineNameField;
        private void Awake()
        {
            _splineSaver = GetComponent<ISplineSaver>();

            if (_splineSaver == null)
            {
                Debug.LogError("Не удалось получить интерфейс ISplineSerializator");
            }

            _splineNameField = GameObjectExtension.GetComponentByObjectName<InputField>("SplineNameInput");
        }

        public void OnCreateSpline()
        {
            Instantiate(SplineObject);
        }

        public void OnDeleteSpline()
        {
            DeleteSelectedSpline();

            SelectRemainSpline();
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
            if (SelectionManager.SelectedSpline != null)
            {
                SplineHolder.Splines.Remove(SelectionManager.SelectedSpline);
                Destroy(SelectionManager.SelectedSpline.gameObject);
            }
        }

        public void OnSaveSpline()
        {
            if (SelectionManager.SelectedSpline == null) return;

            _splineSaver.SaveSpline(SelectionManager.SelectedSpline);
        }

        public void OnLoadSpline()
        {
            if (SelectionManager.SelectedSpline == null) return;

            var loadingSplineName = _splineNameField.text;

            _splineSaver.LoadSpline(loadingSplineName);
        }
    }
}
