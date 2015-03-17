using System;
using System.IO;
using Assets.Scripts.Entities;
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

        private void Awake()
        {
            _saveFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public void SaveSpline(Spline spline)
        {
            var fileName = GetSaveFileName(spline.name);

            if (String.IsNullOrEmpty(fileName)) return;

            var serializedSpline = new SerializableSpline(SelectionManager.SelectedSpline);

            SplineXmlSerializer.SerializeSpline(serializedSpline,fileName);
        }

        public void LoadSpline(string splineName)
        {
            var fileName = GetSaveFileName(splineName);

            if (String.IsNullOrEmpty(fileName) || !File.Exists(fileName)) return;

            var loadedSpline = SplineXmlSerializer.DeSerializeSpline(fileName);

            RestoreDeserializedSpline(loadedSpline);
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
