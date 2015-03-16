using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Operators
{
    public class SplineRendered : MonoBehaviour
    {
        public Color ActiveSplineColor = Color.yellow;

        public Color InActiveSplineColor = Color.gray;

        public Color SourceLineColor = Color.white;

        private static Material LineMaterial;

        private static void CreateLineMaterial()
        {
            if (!LineMaterial)
            {
                LineMaterial = new Material("Shader \"Lines/Colored Blended\" {" +
                                            "SubShader { Pass { " +
                                            "    Blend SrcAlpha OneMinusSrcAlpha " +
                                            "    ZWrite Off Cull Off Fog { Mode Off } " +
                                            "    BindChannels {" +
                                            "      Bind \"vertex\", vertex Bind \"color\", color }" +
                                            "} } }");
                LineMaterial.hideFlags = HideFlags.HideAndDontSave;
                LineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
            }
        }

        private void Start()
        {
            CreateLineMaterial();
        }

        private void OnPostRender()
        {
            foreach (var spline in SplineHolder.Splines)
            {
                LineMaterial.SetPass(0);
                GL.Begin(GL.LINES);
 
                if (spline.DrawSourceLine)
                {
                    GL.Color(spline.IsSelected ? SourceLineColor : InActiveSplineColor);

                    for (int i = 0; i < spline.KeyPoints.Count - 1; i++)
                    {
                        DrawLine(spline.KeyPoints[i].Position, spline.KeyPoints[i + 1].Position);
                    }
                    if (spline.IsClosedSpline)
                    {
                        DrawLine(spline.KeyPoints[0].Position, spline.KeyPoints[spline.KeyPoints.Count - 1].Position);
                    }
                }
                GL.Color(spline.IsSelected ? ActiveSplineColor : InActiveSplineColor);
                Debug.Log("Points count: " + spline.SplinePoints.Count);
                for (int i = 0; i < spline.SplinePoints.Count - 1; i++)
                {
                    DrawLine(spline.SplinePoints[i], spline.SplinePoints[i + 1]);
                }

                GL.End();
            }
        }

        private void DrawLine(Vector3 begin, Vector3 end)
        {
            GL.Vertex3(begin.x, begin.y, begin.z);
            GL.Vertex3(end.x, end.y, end.z);
        }
    }
}
