using System.Collections.Generic;

namespace Assets.Scripts.Entities
{
    public class SerializableSpline
    {
        /// <summary>
        /// Конструктор для сериализатора
        /// </summary>
        public SerializableSpline()
        {
            KeyPoints = new List<SerializablePoint>();
        }

        public SerializableSpline(Spline spline)
        {
            InitFromSpline(spline);
        }

        public List<SerializablePoint> KeyPoints { get; set; }

        public int MaxVerticesCurve { get; set; }

        public bool IsClosedSpline { get; set; }

        public bool DrawSourceLine { get; set; }

        public string Name { get; set; }

        public void InitFromSpline(Spline spline)
        {
            InitKeyPoints(spline);

            MaxVerticesCurve = spline.MaxVerticesCurve;

            IsClosedSpline = spline.IsClosedSpline;

            DrawSourceLine = spline.DrawSourceLine;

            Name = spline.name;
        }

        private void InitKeyPoints(Spline spline)
        {
            KeyPoints = new List<SerializablePoint>();
            foreach (var keyPoint in spline.KeyPoints)
            {
                KeyPoints.Add(new SerializablePoint(keyPoint));
            }
        }

        public void RestoreSplineProperties(Spline spline)
        {
            spline.name = Name;
            spline.IsClosedSpline = IsClosedSpline;
            spline.DrawSourceLine = DrawSourceLine;
            spline.MaxVerticesCurve = MaxVerticesCurve;
        }


    }
}
