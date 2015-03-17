using System.Collections.Generic;
using Assets.Scripts.Controllers;
using Assets.Scripts.Extensions;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Spline : MonoBehaviour, ISelectableObject
    {
        #region - Constants -

        /// <summary>
        /// Минимальное количество точек спланйа
        /// </summary>
        public const int MinSplinePoints = 3;

        #endregion

        #region - Fields -

        /// <summary>
        /// Коллекция исходных точек сплайна
        /// </summary>
        public List<Point> KeyPoints;

        public bool IsClosedSpline = true;

        public int MaxVerticesCurve = 50;

        public bool DrawSourceLine = true;
        public GameObject PointPrefab;

        private ISplineCalculator _splineCalculator;

        #endregion

        #region - Event Handlers -

        private void Awake()
        {
            _splineCalculator = this.GetComponentEx<ISplineCalculator>();
        }

        // Use this for initialization
        private void Start()
        {
            SelectionManager.SelectedSpline = this;

            SelectFirstKeyPoint();

            SplineHolder.Splines.Add(this);

            SplinePoints = new List<Vector3>();
        }

        private void Update()
        {
            SplinePoints = _splineCalculator.GetSplinePoints();

            if (IsSelected && Input.GetMouseButtonDown(1))
            {
                var pointLocation = _splineCalculator.GetPointLocationFromClick();

                if (pointLocation != Vector3.zero)
                {
                    CreatePointByLocation(pointLocation);
                }
            }
        }

        #endregion

        #region - Properties -

        /// <summary>
        /// Коллекция интерполированных точек.
        /// </summary>
        public List<Vector3> SplinePoints { get; private set; }

        #endregion

        #region - Methods -

        /// <summary>
        /// Выделяет первую точку сплайна.
        /// </summary>
        public void SelectFirstKeyPoint()
        {
            if (KeyPoints.Count > 0)
            {
                SelectionManager.SelectedPoint = KeyPoints[0];
            }
        }

        /// <summary>
        /// Создаёт точку в заданной локации
        /// </summary>
        /// <param name="pointLocation">локация для создания точки</param>
        private void CreatePointByLocation(Vector3 pointLocation)
        {
            var point = Instantiate(PointPrefab);
            point.transform.parent = transform;
            var curentPos = CameraController.MainCamera.ScreenToWorldPoint(pointLocation);
            point.transform.position = curentPos;
            KeyPoints.Add(point.GetComponent<Point>());
        }

        #endregion

        #region - ISelectable Object Members -

        /// <summary>
        /// Выделяет компонент
        /// </summary>
        public void Select()
        {
            SelectionManager.SelectedSpline = this;
        }

        /// <summary>
        /// Является ли компонент выделенным.
        /// </summary>
        public bool IsSelected
        {
            get { return ReferenceEquals(SelectionManager.SelectedSpline, this); }
        }

        #endregion
    }
}
