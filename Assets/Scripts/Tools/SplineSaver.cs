using System;
using System.IO;
using Assets.Scripts.Controllers;
using Assets.Scripts.Entities;
using Assets.Scripts.Extensions;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public class SplineSaver : MonoBehaviour, ISplineSaver
    {
        #region - Fields -

        private const string SplineFileNameExtension = "spl";

        public GameObject PointPrefab;

        public GameObject SplinePrefab;

        private string _saveFolderPath;

        private StatusBarController _statusBarController;

        #endregion

        #region - Start Inicialization -

        private void Awake()
        {
            InitSavePath();

            _statusBarController = this.GetComponentEx<StatusBarController>();
        }

        /// <summary>
        /// Инициализирует путь к папке с сохранениями.
        /// </summary>
        private void InitSavePath()
        {
            _saveFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "SavedSplines");

            if (!Directory.Exists(_saveFolderPath))
            {
                Directory.CreateDirectory(_saveFolderPath);
            }
        }

        #endregion

        #region - ISplineSaver Methods -

        /// <summary>
        /// Сохраняет спалайн
        /// </summary>
        /// <param name="spline">сплайн для сохранения</param>
        public void SaveSpline(Spline spline)
        {
            var fileName = GetSplineFilePath(spline.name);

            if (String.IsNullOrEmpty(fileName))
            {
                _statusBarController.UpdateStatus("Can't save spline with empty name");

                return;
            }

            var serializedSpline = new SerializableSpline(SelectionManager.SelectedSpline);

            SplineXmlSerializer.SerializeSpline(serializedSpline, fileName);

            _statusBarController.UpdateStatus(string.Format("Spline \"{0}\" was succesfully saved", spline.name));
        }

        /// <summary>
        /// Загружает сплайн
        /// </summary>
        /// <param name="splineName">имя загружаемого сплайна</param>
        public void LoadSpline(string splineName)
        {
            var fileName = GetSplineFilePath(splineName);

            if (!File.Exists(fileName))
            {
                _statusBarController.UpdateStatus(string.Format("Can't find spline with name \"{0}\"", splineName));

                return;
            }

            var loadedSpline = SplineXmlSerializer.DeSerializeSpline(fileName);

            if (loadedSpline == null)
            {
                _statusBarController.UpdateStatus(string.Format("Can't load file for spline \"{0}\"", splineName));

                return;
            }

            RestoreFromDeserializedSpline(loadedSpline);

            _statusBarController.UpdateStatus(string.Format("Spline \"{0}\" was succesfully loaded", splineName));
        }

        /// <summary>
        /// Получает путь до файла сплайна
        /// </summary>
        /// <param name="splineName">имя сохраняемого сплайна</param>
        private string GetSplineFilePath(string splineName)
        {
            return Path.Combine(_saveFolderPath, GetSplineFileName(splineName));
        }

        /// <summary>
        /// Получает имя файла сплайна
        /// </summary>
        private string GetSplineFileName(string splineName)
        {
            return string.Concat(splineName, ".", SplineFileNameExtension);
        }

        /// <summary>
        /// Выстанавливает Spline из SerializableSpline
        /// </summary>
        private void RestoreFromDeserializedSpline(SerializableSpline serializableSpline)
        {
            var splineObj = Instantiate(SplinePrefab);

            var spline = splineObj.GetComponent<Spline>();

            serializableSpline.RestoreSplineProperties(spline);

            for (var i = 0; i < serializableSpline.KeyPoints.Count; i++)
            {
                var serPoint = serializableSpline.KeyPoints[i];

                Point point;

                if (i < Spline.MinSplinePoints)
                {
                    point = spline.KeyPoints[i];
                }
                else
                {
                    var pointObj = Instantiate(PointPrefab);

                    point = pointObj.GetComponent<Point>();

                    point.transform.parent = spline.transform;

                    spline.KeyPoints.Add(point);
                }

                serPoint.RestorePoint(point);
            }
        }

        #endregion
    }
}
