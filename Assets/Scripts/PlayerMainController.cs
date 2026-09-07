using System;
using UnityEngine;

public class PlayerMainController : MonoBehaviour
{
    [SerializeField] private PlayerConfig _config;

    private PlayerMoveController _moveController;
    private PlayerLook _lookController;
    private PlayerInteract _interactController;

    private void Awake()
    {
        _moveController = GetComponent<PlayerMoveController>();
        _lookController = GetComponent<PlayerLook>();
        _interactController = GetComponent<PlayerInteract>();

        if (_config == null)
            return;

        _moveController.Initialize(_config);
        _lookController.Initialize(_config);
        _interactController.Initialize(_config);
    }
}