using System;
using DefaultNamespace;
using UnityEngine;

public class PlayerInteract : PlayerInput, IInputable
{
    [SerializeField] private Transform _handPos;

    private IInteractable _item;
    private IInputable _inputableImplementation;

    private void OnEnable()
    {
        _inputHandler.OnInteractPresssed += Interact;
        _inputHandler.SetInput(this);
    }


    private void Interact()
    {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out var hit, 4f))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var obj))
            {
                Debug.Log("Ткнул");
                obj.Interact();
            }
        }
    }

    public void Run()
    {
    }
}