using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class SerializablePoint
    {
        #region - Constructors -

        /// <summary>
        /// Конструктор для сериализатора.
        /// </summary>
        public SerializablePoint()
        {

        }

        /// <summary>
        /// Создаёт объект на основе Point
        /// </summary>
        /// <param name="point">исходный Point</param>
        public SerializablePoint(Point point)
        {
            InitFromPoint(point);
        }

        #endregion

        #region - Properties -

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public float Bias { get; set; }

        public float Tension { get; set; }

        public float Continuity { get; set; }

        #endregion

        #region - Methods -

        /// <summary>
        /// Инициазирует свойства текущего объекта из Point
        /// </summary>
        /// <param name="point">исходный Point</param>
        public void InitFromPoint(Point point)
        {
            X = point.Position.x;
            Y = point.Position.y;
            Z = point.Position.z;

            Bias = point.Bias;
            Tension = point.Tension;
            Continuity = point.Continuity;
        }

        /// <summary>
        /// Восстанавливает свойства Point из текущего объекта
        /// </summary>
        /// <param name="point">Point для восстановления</param>
        public void RestorePoint(Point point)
        {
            point.Position = new Vector3(X, Y, Z);

            point.Bias = Bias;

            point.Tension = Tension;

            point.Continuity = Continuity;
        }

        #endregion
    }
}
