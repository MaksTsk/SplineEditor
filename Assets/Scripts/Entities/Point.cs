using Assets.Scripts.Behaviours;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Operators;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Point : MonoBehaviour, ISelectableObject
    {
        private SelectMaterials _selectMaterials;

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

        public bool IsSelected
        {
            get { return ReferenceEquals(Selector.SelectedPoint, this); }
        }

        public void Select()
        {
            Debug.Log("Point Select");

            var parentObject = GetComponentInParent<Spline>();

            if (parentObject != null)
            {
                parentObject.Select();
            }

            Selector.SelectedPoint = this;
        }

        public float Tension;

        public float Bias;

        public float Continuity;

        public Vector3 Position { get { return transform.position; } }
    }
}
