using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public class SplineRendered : MonoBehaviour
    {
        #region - Fields -

        /// <summary>
        /// Цвет выделенного сплайна
        /// </summary>
        public Color ActiveSplineColor = Color.yellow;

        /// <summary>
        /// Цвет невыделенного сплайна
        /// </summary>
        public Color InActiveSplineColor = Color.gray;

        /// <summary>
        /// Цвет прямой линии
        /// </summary>
        public Color SourceLineColor = Color.white;

        /// <summary>
        /// Материал сплайна
        /// </summary>
        private static Material _lineMaterial;

        #endregion

        #region - Start Inicialization -

        /// <summary>
        /// Создаёт материал сплайна для отрисовки
        /// </summary>
        private static void CreateLineMaterial()
        {
            if (!_lineMaterial)
            {
                _lineMaterial = new Material("Shader \"Lines/Colored Blended\" {" +
                                             "SubShader { Pass { " +
                                             "    Blend SrcAlpha OneMinusSrcAlpha " +
                                             "    ZWrite Off Cull Off Fog { Mode Off } " +
                                             "    BindChannels {" +
                                             "      Bind \"vertex\", vertex Bind \"color\", color }" +
                                             "} } }");
                _lineMaterial.hideFlags = HideFlags.HideAndDontSave;
                _lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
            }
        }

        private void Start()
        {
            CreateLineMaterial();
        }

        #endregion

        #region - Rendering -

        private void OnPostRender()
        {
            foreach (var spline in SplineHolder.Splines)
            {
                _lineMaterial.SetPass(0);
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

                for (var i = 0; i < spline.SplinePoints.Count - 1; i++)
                {
                    DrawLine(spline.SplinePoints[i], spline.SplinePoints[i + 1]);
                }

                GL.End();
            }
        }

        /// <summary>
        /// Рисует линию между двумя точками
        /// </summary>
        /// <param name="begin">точка начала</param>
        /// <param name="end">точка окончания</param>
        private void DrawLine(Vector3 begin, Vector3 end)
        {
            GL.Vertex3(begin.x, begin.y, begin.z);
            GL.Vertex3(end.x, end.y, end.z);
        }

        #endregion
    }
}
