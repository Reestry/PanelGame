using System;
using UnityEngine;

public enum MoveState
{
    Walk,
    Sprint,
    Crouch
}

[RequireComponent(typeof(CharacterController))]
public class PlayerController : PlayerInput, IInputable
{
    [Header("Movement")]
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _sprintSpeed = 10f;
    [SerializeField] private float _crouchSpeed = 2f;
    [SerializeField] private float _airControl = 0.3f; 

    [Header("Jump & Gravity")]
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _itemPushForce = 10f;

    private CharacterController _characterController;
    private Vector2 _inputVector;
    private Vector3 _velocity; 
    private MoveState _moveState;

    private void OnEnable()
    {
        _characterController = GetComponent<CharacterController>();
        _inputHandler.SetInput(this);
        _inputHandler.OnMoveHandler += GetMove;
        SetState(MoveState.Walk);
    }

    private void OnDisable()
    {
        _inputHandler.OnMoveHandler -= GetMove;
    }

    public void Run()
    {
        HandleState();
        HandleMovement();
    }

    private void HandleMovement()
    {
        var isGrounded = _characterController.isGrounded;

        if (isGrounded && _velocity.y < 0)
            _velocity.y = -2f;
        
        var targetDirection = transform.right * _inputVector.x + transform.forward * _inputVector.y;
        var currentSpeed = GetSpeedForState();

        // На земле
        if (isGrounded)
        {
            _velocity.x = targetDirection.x * currentSpeed;
            _velocity.z = targetDirection.z * currentSpeed;
            
            if (_inputHandler.ReturnHandler().Player.Jump.WasPressedThisFrame())
            {
                _velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);
            }
        }
        // В воздухе. Сохранение инерции
        else
        {
            var airMove = targetDirection * currentSpeed * _airControl;
            _velocity.x = Mathf.Lerp(_velocity.x, airMove.x, Time.deltaTime * 2f);
            _velocity.z = Mathf.Lerp(_velocity.z, airMove.z, Time.deltaTime * 2f);
        }
        
        _velocity.y += _gravity * Time.deltaTime;
        
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void HandleState()
    {
        if (!_characterController.isGrounded) return;

        if (_inputHandler.ReturnHandler().Player.Sprint.IsPressed() && _moveState == MoveState.Walk)
            SetState(MoveState.Sprint);

        if (_inputHandler.ReturnHandler().Player.Sprint.WasReleasedThisFrame() && _moveState == MoveState.Sprint)
            SetState(MoveState.Walk);
    }

    private float GetSpeedForState() => _moveState switch
    {
        MoveState.Sprint => _sprintSpeed,
        MoveState.Crouch => _crouchSpeed,
        _ => _walkSpeed
    };

    private void SetState(MoveState state) => _moveState = state;

    private void GetMove(Vector2 move) => _inputVector = move;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var obj = hit.collider.attachedRigidbody;
        if (obj == null || obj.isKinematic) return;

        obj.WakeUp();
        obj.AddForce(hit.moveDirection * _itemPushForce * Time.deltaTime, ForceMode.Impulse);
    }
}