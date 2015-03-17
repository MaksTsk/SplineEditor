using Assets.Scripts.Entities;

namespace Assets.Scripts.Controllers
{
    public static class SelectionManager
    {
        /// <summary>
        /// Выделенная точка
        /// </summary>
        public static Point SelectedPoint { get; set; }

        /// <summary>
        /// Выделенный сплайн
        /// </summary>
        public static Spline SelectedSpline { get; set; }
    }
}
