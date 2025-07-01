using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class BoxComponent : MonoBehaviour
    {
        public BoxType Type;
        public event Action<BoxComponent> MouseButtonDrag;
        public event Action<BoxComponent> MouseButtonEnter;
        public event Action MouseButtonExit;
        public event Action MouseButtonUp;

        public FixedJoint fixedJoint;
        public Rigidbody forFixedJoint;
        public int layer;
        public float highOfGrab =0.5f;


        public GameObject ObjectInside { get; set; }

        private void OnMouseDown()
        {
            PickUpBox();
            MouseButtonDrag?.Invoke(this);
        }

        private void OnMouseUp()
        {
            DropBox();
            MouseButtonUp?.Invoke();
        }

        private void OnMouseEnter()
        {
            MouseButtonEnter?.Invoke(this);
        }

        private void OnMouseExit()
        {
            MouseButtonExit?.Invoke();
        }


        public void DropBox()
        {
            SetOutline(0);
            SetDropPosition();
        }

        private void SetDropPosition()
        {
            transform.position += new Vector3(0, 0.3f, 0);
            fixedJoint.connectedBody = forFixedJoint;
        }

        private void PickUpBox()
        {
            SetOutline(layer);
        }

        private void SetOutline(int layer)
        {
            gameObject.layer = layer;
        }
    }
}