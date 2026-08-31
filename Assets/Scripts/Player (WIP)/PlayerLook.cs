using System;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerLook : PlayerInput, IInputable
{
    [SerializeField] private float _mouseSens = 5;
    [SerializeField] private CinemachineCamera _mainCamera;

    private Vector2 _looking;
    private float xrotation;
    private float _lookOffset = 90;

    private void OnEnable()
    {
        _inputHandler.OnLookHandler += GetLook;
        _inputHandler.SetInput(this);
    }

    private void GetLook(Vector2 look)
    {
        _looking = look;
    }

    public void Run()
    {
        Looking();
    }

    private void Looking()
    {
        transform.Rotate(_looking.x * transform.up * _mouseSens * Time.deltaTime);

        xrotation -= _looking.y * _mouseSens * Time.deltaTime;
        xrotation = Mathf.Clamp(xrotation, -_lookOffset, _lookOffset);
        _mainCamera.transform.localRotation = Quaternion.Euler(xrotation, 0, 0);
    }
}