using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Operators
{
    public class SplineRendered : MonoBehaviour
    {
        public Material LineMaterial;

        // Use this for initialization
        void Start () {
	
        }

        void OnPostRender()
        {
           Debug.Log(string.Format("OnPostRender: lines:{0}",SplineHolder.Splines.Count));
            foreach (var spline in SplineHolder.Splines)
            {
                LineMaterial.SetPass(0);
                GL.Begin(GL.LINES);

                if (spline.DrawSourceLine)
                {
                    GL.Color(spline.IsSelected ? Color.white : new Color(1, 1, 1, 0.4f));

                    for (int i = 0; i < spline.KeyPoints.Count - 1; i++)
                    {
                        DrawLine(spline.KeyPoints[i].Position, spline.KeyPoints[i + 1].Position);
                    }
                    if (spline.IsClosedSpline)
                    {
                        DrawLine(spline.KeyPoints[0].Position, spline.KeyPoints[spline.KeyPoints.Count - 1].Position);
                    }
                }

                GL.Color(spline.IsSelected ? Color.red : new Color(1, 1, 1, 0.4f));
                Debug.Log("Points count: " + spline.SplinePoints.Count);
                for (int i = 0; i < spline.SplinePoints.Count - 1; i++)
                {
                    DrawLine(spline.SplinePoints[i], spline.SplinePoints[i + 1]);
                }

                GL.End();
            }
        }

        void DrawLine(Vector3 begin, Vector3 end)
        {
            GL.Vertex3(begin.x, begin.y, begin.z);
            GL.Vertex3(end.x, end.y, end.z);
        }
    }
}
