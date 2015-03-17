using System.Collections.Generic;
using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class SplineHolder : MonoBehaviour
    {
        private static List<Spline> _splines;

        /// <summary>
        /// Коллекция сплайнов.
        /// </summary>
        public static List<Spline> Splines
        {
            get
            {
                if (_splines == null)
                {
                    _splines = new List<Spline>();
                }
                return _splines;
            }

            set { _splines = value; }
        }
    }
}
