using System.Collections.Generic;

namespace Assets.Scripts.Entities
{
    public class SerializableSpline
    {
        #region - Constructors -

        /// <summary>
        /// Конструктор для сериализатора
        /// </summary>
        public SerializableSpline()
        {
            KeyPoints = new List<SerializablePoint>();
        }

        /// <summary>
        /// Создает объект на основе Spline
        /// </summary>
        /// <param name="spline">исходный Spline</param>
        public SerializableSpline(Spline spline)
        {
            InitFromSpline(spline);
        }

        #endregion

        #region - Properties -

        public List<SerializablePoint> KeyPoints { get; set; }

        public int MaxVerticesCurve { get; set; }

        public bool IsClosedSpline { get; set; }

        public bool DrawSourceLine { get; set; }

        public string Name { get; set; }

        #endregion

        #region - Methods -

        /// <summary>
        /// Инициализирует текущий объект из Spline
        /// </summary>
        /// <param name="spline">исходный Spline</param>
        public void InitFromSpline(Spline spline)
        {
            InitKeyPoints(spline.KeyPoints);

            MaxVerticesCurve = spline.MaxVerticesCurve;

            IsClosedSpline = spline.IsClosedSpline;

            DrawSourceLine = spline.DrawSourceLine;

            Name = spline.name;
        }

        /// <summary>
        /// Инициализует точки объекта на основе точек сплайна
        /// </summary>
        /// <param name="points">исходная коллекция точек сплайна</param>
        private void InitKeyPoints(IEnumerable<Point> points)
        {
            KeyPoints = new List<SerializablePoint>();
            foreach (var keyPoint in points)
            {
                KeyPoints.Add(new SerializablePoint(keyPoint));
            }
        }

        /// <summary>
        /// Восстанавливает свойства сплайна из текущего объекта
        /// </summary>
        /// <param name="spline">сплайн для восстановления</param>
        public void RestoreSplineProperties(Spline spline)
        {
            spline.name = Name;
            spline.IsClosedSpline = IsClosedSpline;
            spline.DrawSourceLine = DrawSourceLine;
            spline.MaxVerticesCurve = MaxVerticesCurve;
        }

        #endregion
    }
}
