using System;
using DefaultNamespace;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractableIcon _interactableIcon;

    private InteractableIcon _icon;
    private bool _hasTriggered;

    private void Start()
    {
        _icon = CreateInteractableIcon(transform);
        _icon.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            _icon.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Для оптимизации
        if (_hasTriggered)
            return;

        if (other.GetComponent<PlayerController>())
        {
            _icon.gameObject.SetActive(true);
            _hasTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            _icon.gameObject.SetActive(false);
            _hasTriggered = false;
        }
    }

    private InteractableIcon CreateInteractableIcon(Transform target)
    {
        var icon = Instantiate(_interactableIcon, GameplayManagerUI.Instance.transform);
        icon.SetTarget(target);

        return icon;
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