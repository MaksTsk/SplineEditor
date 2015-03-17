using System.IO;
using Assets.Scripts.Entities;
using Assets.Scripts.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Operators
{
    public class SplineManager : MonoBehaviour
    {
        private const string SplineFileNameExtension = "spl";

        public GameObject SplineObject;

        public GameObject PointPrefab;

        private ISplineSerializator _splineSerializator;

        private void Awake()
        {
            _splineSerializator = GetComponent<ISplineSerializator>();

            if (_splineSerializator == null)
            {
                Debug.LogError("Не удалось получить интерфейс ISplineSerializator");
            }
        }

        public void OnCreateSpline()
        {
            Instantiate(SplineObject, new Vector3(0, 0, 0), Quaternion.identity);
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

            var fileName = GetSaveFileName();

            if (string.IsNullOrEmpty(fileName)) return;

            var serializedSpline = new SerializableSpline(SelectionManager.SelectedSpline);

            _splineSerializator.SerializeSpline(serializedSpline, fileName);
        }

        private string GetSaveFileName()
        {
            return EditorUtility.SaveFilePanel(
                "Save Spline", 
                string.Empty,
                string.Concat(SelectionManager.SelectedSpline.name,".",SplineFileNameExtension), 
                SplineFileNameExtension);
        }

        public void OnLoadSpline()
        {
            var fileName = GetLoadFileName();

            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName)) return;

            var loadedSpline = _splineSerializator.DeSerializeSpline(fileName);

            RestoreDeserializedSpline(loadedSpline);
        }

        private void RestoreDeserializedSpline(SerializableSpline serializableSpline)
        {
            var splineObj = Instantiate(SplineObject);

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

        private string GetLoadFileName()
        {
            return EditorUtility.OpenFilePanel(
                "Load Spline",
                string.Empty,
                SplineFileNameExtension);
        }
    }
}
