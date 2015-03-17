using Assets.Scripts.Entities;

namespace Assets.Scripts.Interfaces
{
    public interface ISplineSaver
    {
        void SaveSpline(Spline spline);
        
        void LoadSpline(string splineName);

    }
}
