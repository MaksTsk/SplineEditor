using System;
using System.IO;
using Assets.Scripts.Controllers;
using Assets.Scripts.Entities;
using Assets.Scripts.Extensions;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Operators
{
    public class SplineSaver : MonoBehaviour, ISplineSaver
    {
        private const string SplineFileNameExtension = "spl";

        public GameObject PointPrefab;

        public GameObject SplinePrefab;

        private string _saveFolderPath;

        private StatusBarController _statusBarController;

        private void Awake()
        {
            InitSavePath();

            _statusBarController = this.GetComponentEx<StatusBarController>();
        }

        private void InitSavePath()
        {
            _saveFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SavedSplines");

            if (!Directory.Exists(_saveFolderPath))
            {
                Directory.CreateDirectory(_saveFolderPath);
            }
        }

        public void SaveSpline(Spline spline)
        {
            var fileName = GetSaveFileName(spline.name);

            if (String.IsNullOrEmpty(fileName))
            {
                _statusBarController.UpdateStatus("Can't save spline with empty name");
                
                return;
            }

            var serializedSpline = new SerializableSpline(SelectionManager.SelectedSpline);

            SplineXmlSerializer.SerializeSpline(serializedSpline,fileName);

            _statusBarController.UpdateStatus("Spline was succesfully saved");
        }

        public void LoadSpline(string splineName)
        {
            var fileName = GetSaveFileName(splineName);

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

            RestoreDeserializedSpline(loadedSpline);

            _statusBarController.UpdateStatus(string.Format("Spline \"{0}\" was succesfully loaded", splineName));
        }

        private string GetSaveFileName(string splineName)
        {
            return Path.Combine(_saveFolderPath, GetFileNameBySplineName(splineName));
        }

        private string GetFileNameBySplineName(string splineName)
        {
            return string.Concat(splineName, ".", SplineFileNameExtension);
        }

        private void RestoreDeserializedSpline(SerializableSpline serializableSpline)
        {
            var splineObj = Instantiate(SplinePrefab);

            var spline = splineObj.GetComponent<Spline>();

            serializableSpline.RestoreSplineProperties(spline);

            for (var i = 0; i < serializableSpline.KeyPoints.Count; i++)
            {
                var serPoint = serializableSpline.KeyPoints[i];

                Point point;

                if (i < Spline.DefaultPointsCount)
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
    }
}
