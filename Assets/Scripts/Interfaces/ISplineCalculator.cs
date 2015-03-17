using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface ISplineCalculator
    {
        List<Vector3> GetSplinePoints();

        Vector3 GetPointLocationFromClick();
    }
}
