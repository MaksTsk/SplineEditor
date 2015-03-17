using UnityEngine;

namespace Assets.Scripts.Operators
{
    public class SplineManager : MonoBehaviour
    {
        public GameObject SplineObject;

        public void OnCreateSpline()
        {
            Instantiate(SplineObject, new Vector3(0, 0, 0), Quaternion.identity);
        }

        public void OnDeleteSpline()
        {
            DeleteSelectedSpline();

            SelectRemainSpline();
        }

        private void SelectRemainSpline()
        {
            if (SplineHolder.Splines.Count > 0)
            {
                var firstSpline = SplineHolder.Splines[0];

                SelectionManager.SelectedSpline = firstSpline;

                firstSpline.SelectFirstKeyPoint();
            }
        }

        private void DeleteSelectedSpline()
        {
            if (SelectionManager.SelectedSpline != null)
            {
                SplineHolder.Splines.Remove(SelectionManager.SelectedSpline);
                Destroy(SelectionManager.SelectedSpline.gameObject);
            }
        }

        public void OnSaveSpline()
        {
            
        }

        public void OnLoadSpline()
        {
            
        }
    }
}
