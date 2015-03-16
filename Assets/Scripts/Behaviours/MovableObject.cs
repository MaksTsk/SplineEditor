﻿using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(MeshCollider))]
    public class MovableObject : MonoBehaviour {

        private Vector3 _screenPoint;
        private Vector3 _offset;

        private void OnMouseDown()
        {
            _screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            _offset = gameObject.transform.position -
                      Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));

        }

        private void OnMouseDrag()
        {
            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);

            var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;

            transform.position = curPosition;
        }
    }
}
