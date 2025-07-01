using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class ControlItems : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Camera camera;
    [SerializeField] private Rigidbody hookRb;
    [SerializeField] private Transform _hookTransform;

    private BoxComponent hookedBox;
    
    private bool isHooked;
    private float _defaultSpeed = 15f;
    private int _speedWithoutBox = 1000;

    private void Start()
    {
        isHooked = false;
    }

    void FixedUpdate()
    {
        MoveHook();
    }

    public void HookBox(BoxComponent boxComponent)
    {
        if (isHooked) return;
        SetBoxParametrs(boxComponent);
        isHooked = true;
        speed = _defaultSpeed;
    }

    private void MoveHook()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(camera.transform.position, hit.point, Color.green);
            hookRb.position = Vector3.MoveTowards(_hookTransform.position,
                new Vector3(hit.point.x, _hookTransform.position.y, hit.point.z), speed * Time.deltaTime);
        }
    }

    private void SetBoxParametrs(BoxComponent boxComponent)
    {
        hookedBox = boxComponent;
        boxComponent.transform.position = new Vector3(hookRb.position.x, hookRb.position.y - boxComponent.highOfGrab,
            hookRb.position.z);
        boxComponent.fixedJoint.connectedBody = hookRb;
    }

    public void DropBox()
    {
        isHooked = false;
        hookedBox.transform.position += new Vector3(0, 0.3f, 0);
        hookedBox.fixedJoint.connectedBody = hookedBox.forFixedJoint;
        speed = _speedWithoutBox;
        hookedBox = null;
    }
}