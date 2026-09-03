using System;
using DefaultNamespace;
using UnityEngine;

public class TakeableItem : MonoBehaviour, IInteractable
{
    private InteractableIconObject _icon;

    private void Start()
    {
        _icon = GetComponentInChildren<InteractableIconObject>();
    }

    public void Interact()
    {
        if (_icon != null)
            _icon.gameObject.SetActive(false);
    }

    public void Release()
    {
        if (_icon != null)
            _icon.gameObject.SetActive(true);
    }
}
