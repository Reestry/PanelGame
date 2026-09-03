using System;
using DefaultNamespace;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractableIcon _interactableIcon;

    private InteractableIcon _icon;

    private void OnEnable()
    {
        _icon = CreateInteractableIcon(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            _icon = CreateInteractableIcon(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            DestroyInteractableIcon();
        }
    }


    private InteractableIcon CreateInteractableIcon(Transform target)
    {
        var icon = Instantiate(_interactableIcon, GameplayManagerUI.Instance.transform);
        icon.SetTarget(target);

        return icon;
    }

    private void DestroyInteractableIcon()
    {
        Destroy(_icon.gameObject);
    }

    public void Interact()
    {
        _icon.gameObject.SetActive(false);
    }

    public void Release()
    {
        _icon.gameObject.SetActive(true);
    }
}