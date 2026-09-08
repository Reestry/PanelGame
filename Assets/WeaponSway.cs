using System;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    private InputHandler _playerInput;

    [SerializeField] private Transform _targetWeapon;
    
    private Vector2 _rotation;

    // Конфиг
    [SerializeField] private bool InvertX;
    [SerializeField] private bool InvertY;
    [SerializeField] private float _swayMult;
    [SerializeField] private float _smooth;

    private int _invertXMultiplier = 1;
    private int _invertYMultiplier = 1;

    
    
    private void Awake()
    {
        _playerInput = GetComponent<InputHandler>();



    }

    private void LateUpdate()
    {
        _invertXMultiplier = InvertX ? -1 : 1;
        _invertYMultiplier = InvertY ? -1 : 1;
        
        
        _rotation = _playerInput.ReturnHandler().Player.Look.ReadValue<Vector2>() * _swayMult;

        var rotationY = Quaternion.AngleAxis((_rotation.x / 2) * _invertYMultiplier, Vector3.up);
        var rotationX = Quaternion.AngleAxis(_rotation.y * _invertXMultiplier, Vector3.right);

        var target = rotationX * rotationY;

        _targetWeapon.localRotation =
            Quaternion.Slerp(_targetWeapon.localRotation, target, _smooth * Time.deltaTime);
    }
}
