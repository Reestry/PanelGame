using System;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action<Vector2> OnMoveHandler;
    public event Action<Vector2> OnLookHandler;
    public event Action OnJumpHandler;

    public event Action OnMoveStarted;
    public event Action OnMoveEnded;
    
    public event Action OnInteractPresssed;

    private InputSystem_Actions _inputSystem;

    private List<IInputable> _inputables = new();

    public void SetInput(IInputable input)
    {
        _inputables.Add(input);
    }

    public void ClearInput(IInputable input)
    {
        _inputables.Remove(input);
    }

    private void Awake()
    {
        _inputSystem = new InputSystem_Actions();
        _inputSystem.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _inputSystem.Player.Jump.performed += ctx => { OnJumpHandler?.Invoke(); };

        _inputSystem.Player.Interact.started += ctx => { OnInteractPresssed?.Invoke(); };
    }

    
    private void OnDisable()
    {
        _inputSystem.Disable();
    }

    public InputSystem_Actions ReturnHandler()
    {
        return _inputSystem;
    }

    void Update()
    {
        var look = _inputSystem.Player.Look.ReadValue<Vector2>();
        OnLookHandler?.Invoke(look);

        foreach (var input in _inputables)
            input.Run();
    }

    private void OnEnable()
    {
        MoveHandler();
    }

    private void MoveHandler()
    {
        _inputSystem.Player.Move.started += ctx => { OnMoveStarted?.Invoke(); };

        _inputSystem.Player.Move.performed += ctx =>
        {
            var move = ctx.ReadValue<Vector2>();
            OnMoveHandler?.Invoke(move);
        };

        _inputSystem.Player.Move.canceled += ctx =>
        {
            OnMoveHandler?.Invoke(Vector2.zero);

            OnMoveEnded?.Invoke();
        };
    }
}