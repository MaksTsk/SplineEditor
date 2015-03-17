using Assets.Scripts.Entities;

namespace Assets.Scripts.Interfaces
{
    public interface ISplineSerializator
    {
        void SerializeSpline(SerializableSpline spline, string fileName);

        SerializableSpline DeSerializeSpline(string fileName);
    }
}
