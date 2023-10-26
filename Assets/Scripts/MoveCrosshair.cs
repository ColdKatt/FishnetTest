using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FishNet.Object;

public class MoveCrosshair : NetworkBehaviour
{
    private Click _clickActions;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(FindObjectOfType<CrosshairCanvas>().transform);

        Cursor.visible = false;

        _clickActions = new Click();
        _clickActions.Mouse.Click.Enable();

        _clickActions.Mouse.Click.started += Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if (!base.IsOwner) return;

        var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Physics.Raycast(ray, out RaycastHit hitInfo);

        if (hitInfo.collider)
        {
            var a = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            a.transform.position = hitInfo.point;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!base.IsOwner) return;

        var a = Mouse.current.position.ReadValue();
        var b = Camera.main.ScreenToWorldPoint(a);
        transform.position = a;
        Debug.Log(a);
    }
}
