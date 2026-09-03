using System;
using DefaultNamespace;
using UnityEngine;

public class PlayerInteract : PlayerInput, IInputable
{
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


        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out var hit, 4f))
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

    [SerializeField] private Transform _handPos;
    [SerializeField] private float _takeForce;

    [SerializeField] private float _maxLenghth = 5f;

    private Rigidbody _item;

    private void FixedUpdate()
    {
        if (_item == null)
            return;

        var direction = _handPos.position - _item.position;

        var distance = direction.magnitude;
        if (distance > _maxLenghth)
        {
            DropItem();
        }

        _item.linearVelocity = direction * _takeForce;
        _item.AddTorque(-_item.angularVelocity * 0.9f, ForceMode.VelocityChange);
    }

    private void DropItem()
    {
        _item = null;
        GameplayManagerUI.Instance.gameObject.SetActive(true);
    }

    public bool HaveItem()
    {
        return _item;
    }

    public void Run()
    {
    }
}