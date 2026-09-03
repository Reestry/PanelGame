using System;
using DefaultNamespace;
using UnityEngine;

public class InteractableIconObject : MonoBehaviour
{
    [SerializeField] private InteractableIcon _interactableIcon;

    private InteractableIcon _icon;
    private bool _hasTriggered;

    private void Start()
    {
        _icon = CreateInteractableIcon(transform);
        HideIcon();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            AppearIcon();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Для оптимизации
        if (_hasTriggered)
            return;

        if (other.GetComponent<PlayerController>())
        {
            AppearIcon();
            _hasTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            HideIcon();
            _hasTriggered = false;
        }
    }

    private InteractableIcon CreateInteractableIcon(Transform target)
    {
        var icon = Instantiate(_interactableIcon, GameplayManagerUI.Instance.transform);
        icon.SetTarget(target);

        return icon;
    }

    public void HideIcon()
    {
        _icon.gameObject.SetActive(false);
    }

    public void AppearIcon()
    {
        _icon.gameObject.SetActive(true);
    }
}