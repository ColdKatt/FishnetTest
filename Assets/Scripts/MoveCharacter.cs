using FishNet.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCharacter : NetworkBehaviour
{
    [SerializeField] private Move _inputActions;
    [SerializeField, Range(1.0f, 10.0f)] private float _speed;

    private float _movementX;
    private float _movementY;

    void Start()
    {
        _inputActions = new Move();

        _inputActions.Keyboard.WASD.Enable();
        _inputActions.Keyboard.WASD.started += DoMove;
        _inputActions.Keyboard.WASD.performed += DoMove;
        _inputActions.Keyboard.WASD.canceled += DoMove;
    }

    private void DoMove(InputAction.CallbackContext context)
    {
        if (!base.IsOwner) return;

        var movementVector = _inputActions.Keyboard.WASD.ReadValue<Vector2>();
        _movementX = movementVector.x;
        _movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        if (!base.IsOwner) return;

        transform.Translate(new Vector3(_movementX, _movementY, 0) * _speed * Time.deltaTime);
    }
}
