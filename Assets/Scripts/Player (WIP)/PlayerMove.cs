using System;
using Unity.Cinemachine;
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
    
    [Header("Camera")]
    [SerializeField] private CinemachineBasicMultiChannelPerlin _cinemachine;
    private float _startAmplitude;
    private float _amplitudeMultiplier;
    [SerializeField] private float _walkAmplitude = 1.5f;
    [SerializeField] private float _sprintAmplitude = 3.5f;
    private float _startFrequency;
    private float _frequencyMultiplier;
    [SerializeField] private float _walkFrequency = 2;
    [SerializeField] private float _sprintFrequency = 4f;
    
    [Header("Movement")] [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _sprintSpeed = 10f;
    [SerializeField] private float _crouchSpeed = 2f;
    [SerializeField] private float _airControl = 0.3f;

    [Header("Jump & Gravity")] [SerializeField]
    private float _jumpForce = 3f;

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

        _startAmplitude = _cinemachine.AmplitudeGain;
        _startFrequency = _cinemachine.FrequencyGain;
    }

    private void Update()
    {
        var isMoving = _inputVector.magnitude >= 0.1f && _characterController.isGrounded;
        _amplitudeMultiplier = GetAmplitudeForState();
        _frequencyMultiplier = GetFrequencyForState();

        var targetAmplitude = isMoving ? _amplitudeMultiplier : _startAmplitude;
        var targetFrequency = isMoving ? _frequencyMultiplier : _startFrequency;
        
        _cinemachine.AmplitudeGain = Mathf.Lerp(
            _cinemachine.AmplitudeGain, 
            targetAmplitude, 
            Time.deltaTime * 5f 
        );
        
        _cinemachine.FrequencyGain = Mathf.Lerp(
            _cinemachine.FrequencyGain, 
            targetFrequency, 
            Time.deltaTime * 5f 
        );
        
        // можно также изменять pivotOffset с 2 на 6 при беге
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
        if (_inputHandler.ReturnHandler().Player.Sprint.WasReleasedThisFrame() && _moveState == MoveState.Sprint)
            SetState(MoveState.Walk);

        if (_characterController.isGrounded)
        {
            if (_inputHandler.ReturnHandler().Player.Sprint.IsPressed() && _moveState == MoveState.Walk)
                SetState(MoveState.Sprint);
        }
    }

    private float GetSpeedForState() =>
        _moveState switch
        {
            MoveState.Sprint => _sprintSpeed,
            MoveState.Crouch => _crouchSpeed,
            _ => _walkSpeed
        };

    private float GetAmplitudeForState() =>
        _moveState switch
        {
            MoveState.Sprint => _sprintAmplitude,
            _ => _walkAmplitude
        };
    
    private float GetFrequencyForState() =>
        _moveState switch
        {
            MoveState.Sprint => _sprintFrequency,
            _ => _walkFrequency
        };

    private void SetState(MoveState state) => _moveState = state;

    private void GetMove(Vector2 move) => _inputVector = move;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var obj = hit.collider.attachedRigidbody;
        if (obj == null || obj.isKinematic)
            return;

        obj.WakeUp();
        obj.AddForce(hit.moveDirection * _itemPushForce * Time.deltaTime, ForceMode.Impulse);
    }
}