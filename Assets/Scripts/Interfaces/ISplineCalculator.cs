using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface ISplineCalculator
    {
        /// <summary>
        /// Получает список интерполированных точек сплайна
        /// </summary>
        List<Vector3> GetSplinePoints();

        /// <summary>
        /// Получает координаты новой точки в зависимости от места клика
        /// </summary>
        Vector3 GetPointLocationFromClick();
    }
}
