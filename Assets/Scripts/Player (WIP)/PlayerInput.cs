
    using System;
    using UnityEngine;

    public class PlayerInput: MonoBehaviour
    {

        protected InputHandler _inputHandler;

        private void Awake()
        {
            _inputHandler = GetComponent<InputHandler>();
        }
    }
