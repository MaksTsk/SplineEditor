using UnityEngine;

namespace Assets.Scripts.Behaviours
{
    /// <summary>
    /// Компонент содержит материалы выделения
    /// </summary>
    public class SelectMaterials : MonoBehaviour
    {
        /// <summary>
        /// Материал неактивного объекта
        /// </summary>
        public Material InActiveMaterial;

        /// <summary>
        /// Материал активного объекта
        /// </summary>
        public Material ActiveMaterial;

        /// <summary>
        /// Материал объекта удерживающего фокус
        /// </summary>
        public Material FocuseMaterial;
    }
}
