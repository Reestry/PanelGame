using System;
using DefaultNamespace;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerInteract : PlayerInput, IInputable
{
    [SerializeField] private Transform _handPos;
    [SerializeField] private float _takeForce;

    [SerializeField] private float _maxLenghth = 5f;

    [SerializeField] private RectTransform _crosshair;

    [SerializeField] private Camera _cam;

    private Rigidbody _item;
    private IInputable _inputableImplementation;

    private void OnEnable()
    {
        _inputHandler.OnInteractPresssed += Interact;
        _inputHandler.SetInput(this);
    }


    private void Interact()
    {
        if (_item != null)
        {
            DropItem();
            return;
        }


        var screenPoint = _crosshair.position;

        var ray = _cam.ScreenPointToRay(screenPoint);

        if (Physics.Raycast(ray, out var hit, 3f, LayerMask.GetMask("Interactable") )) //, QueryTriggerInteraction.Ignore
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var obj))
            {
                Debug.Log("Ткнул");
                obj.Interact();


                if (hit.collider.TryGetComponent<Rigidbody>(out var rb))
                {
                    _item = rb;
                    GameplayManagerUI.Instance.gameObject.SetActive(false);
                }
            }
        }
    }


    private void FixedUpdate()
    {
        if (_item == null)
            return;

        var direction = _handPos.position - _item.position;

        var distance = direction.magnitude;
        if (distance > _maxLenghth)
        {
            DropItem();
            return;
        }

        _item.linearVelocity = direction * _takeForce;
        _item.AddTorque(-_item.angularVelocity * 0.9f, ForceMode.VelocityChange);
    }

    private void DropItem()
    {
        _item.TryGetComponent<IInteractable>(out var interactable);
        interactable.Release();
        _item = null;

        GameplayManagerUI.Instance.gameObject.SetActive(true);
    }


    public void Run()
    {
    }
}