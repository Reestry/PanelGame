// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;

namespace Pause
{
    public class PauseManager : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private Button _pauseButton;
        
        private static readonly List<IPauseHandler> PauseHandlers = new();
        private bool _isPaused;

        public static bool IsPaused { get; private set; }
        public static bool IsPauseWindow { get; private set; }
        public static PauseManager Instance { get; set; }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
            
            _pauseButton.onClick.AddListener(OnPauseButtonClicked);
        }
        
        private void OnPauseButtonClicked()
        {
            SetPaused(!IsPaused);
            OpenPauseWindow();
        }

        public static void Register(IPauseHandler pauseHandler)
        {
            PauseHandlers.Add(pauseHandler);
        }

        public static void UnRegister(IPauseHandler pauseHandler)
        {
            PauseHandlers.Remove(pauseHandler);
        }
        
        public void SetPaused(bool isPaused)
        {
            IsPaused = isPaused;
            foreach (var pauseHandler in PauseHandlers)
                pauseHandler.SetPaused(isPaused);
        }

        private void OpenPauseWindow()
        {
            if (IsPaused)
            {
                IsPauseWindow = true;
                // WindowManager.OpenWindow<PauseWindow>();
            }
            else
            {
                IsPauseWindow = false;
                // WindowManager.CloseLast();
            }
        }

        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
        }
    }
}
