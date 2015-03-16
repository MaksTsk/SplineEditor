using Assets.Scripts.Behaviours;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Point : MonoBehaviour, ISelectableObject
    {
        private SelectMaterials _selectMaterials;

        private void Start()
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
            get { return ReferenceEquals(PointHolder.SelectedPoint, this); }
        }

        public void Select()
        {
            Debug.Log("Point Select");

            var parentObject = GetComponentInParent<Spline>();

            if (parentObject != null)
            {
                parentObject.Select();
            }

            PointHolder.SelectedPoint = this;
        }
    }
}
