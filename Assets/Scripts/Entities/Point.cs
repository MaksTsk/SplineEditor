using Assets.Scripts.Behaviours;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Operators;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Point : MonoBehaviour, ISelectableObject
    {
        #region - Constants -

        public const float DefaultTensionValue = 0;

        public const float DefauleBiasValue = 0;

        public const float DefaultContinuityValue = 0;

        #endregion

        #region - Fields -

        private SelectMaterials _selectMaterials;

        public float Tension = DefaultTensionValue;

        public float Bias = DefauleBiasValue;

        public float Continuity = DefaultContinuityValue;

        #endregion

        #region - Event Handlers -

        private void Awake()
        {
            _selectMaterials = GetComponent<SelectMaterials>();

            if (_selectMaterials == null)
            {
                Debug.LogError("Не назначен SelectMaterials у компонента Point");
            }

        }

        // Update is called once per frame
        private void Update()
        {
            if (transform.parent.GetComponent<ISelectableObject>().IsSelected)
            {
                GetComponent<Collider>().enabled = true;

                GetComponent<Renderer>().sharedMaterial = IsSelected
                    ? _selectMaterials.FocuseMaterial
                    : _selectMaterials.ActiveMaterial;
            }
            else
            {
                GetComponent<Renderer>().sharedMaterial = _selectMaterials.InActiveMaterial;
                GetComponent<Collider>().enabled = false;
            }

        }

        #endregion

        #region - ISelectable Object Members -

        public bool IsSelected
        {
            get { return ReferenceEquals(SelectionManager.SelectedPoint, this); }
        }

        public void Select()
        {
            var parentObject = GetComponentInParent<Spline>();

            if (parentObject != null)
            {
                parentObject.Select();
            }

            SelectionManager.SelectedPoint = this;
        }

        #endregion

        #region - Properties -

        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        #endregion

        #region - Methods -

        /// <summary>
        /// Восстанавливает параметры по умолчанию
        /// </summary>
        public void RestoreDefaultProperties()
        {
            Tension = DefaultTensionValue;

            Bias = DefauleBiasValue;

            Continuity = DefaultContinuityValue;
        }

        #endregion
    }
}
