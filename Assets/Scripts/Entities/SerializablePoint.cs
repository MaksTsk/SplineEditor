using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class SerializablePoint
    {
        public SerializablePoint()
        {

        }

        public SerializablePoint(Point point)
        {
            InitFromPoint(point);
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public float Bias { get; set; }

        public float Tension { get; set; }

        public float Continuity { get; set; }

        public void InitFromPoint(Point point)
        {
            X = point.Position.x;
            Y = point.Position.y;
            Z = point.Position.z;

            Bias = point.Bias;
            Tension = point.Tension;
            Continuity = point.Continuity;
        }

        public void RestorePoint(Point point)
        {
            point.Position = new Vector3(X, Y, Z);

            point.Bias = Bias;

            point.Tension = Tension;

            point.Continuity = Continuity;
        }
    }
}
