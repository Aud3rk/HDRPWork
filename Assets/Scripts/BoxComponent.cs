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

        [SerializeField] private FixedJoint fixedJoint;
        [SerializeField] private Rigidbody forFixedJoint;
        [SerializeField] private int layer;
        [SerializeField] private float highOfGrab;


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

        public void HookBox(Rigidbody hookRigidbody)
        {
            transform.position = new Vector3(hookRigidbody.position.x, transform.position.y + highOfGrab,
                hookRigidbody.position.z);
            fixedJoint.connectedBody = hookRigidbody;
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