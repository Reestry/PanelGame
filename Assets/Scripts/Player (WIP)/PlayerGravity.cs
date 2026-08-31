using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerGravity : PlayerInput, IInputable
{
    [SerializeField] private float _jumpForce = 3;
    private readonly float _gravityScale = -9.8f;
    private Vector3 _velocity;

    private CharacterController _characterController;

    private void OnEnable()
    {
        _characterController = GetComponent<CharacterController>();
        _inputHandler.SetInput(this);
    }

    private void Gravity()
    {
        _velocity.y += _gravityScale * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);

        if (_characterController.isGrounded && _inputHandler.ReturnHandler().Player.Jump.WasPressedThisFrame())
            _velocity.y = _jumpForce;

        if (_characterController.isGrounded && _velocity.y <= 0)
            _velocity.y = -2;
    }

    public void Run()
    {
        Gravity();
    }
}