using Assets.Scripts.Entities;

namespace Assets.Scripts.Interfaces
{
    public interface ISplineSaver
    {
        /// <summary>
        /// Сохраняет спалайн
        /// </summary>
        /// <param name="spline">сплайн для сохранения</param>
        void SaveSpline(Spline spline);
        
        /// <summary>
        /// Загружает сплайн
        /// </summary>
        /// <param name="splineName">имя загружаемого сплайна</param>
        void LoadSpline(string splineName);

    }
}
