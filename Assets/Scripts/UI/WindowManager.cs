// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WindowManager : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage;

        private static Window _currentWindow;
        private static Image _windowBackground;
        private static WindowManager _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
                Destroy(gameObject);
            else
                _instance = this;

            _windowBackground = _backgroundImage;
        }

        public static T OpenWindow<T>() where T : Window
        {
            if (_currentWindow != null)
            {
                _currentWindow.Hide();
                Close(_currentWindow.gameObject);
            }

            if (_windowBackground != null)
                _windowBackground.transform.SetAsLastSibling();

            var window = WindowPool.Get<T>();
            _currentWindow = window;

            window.transform.SetParent(_instance.transform, false);
            window.Show();
            ShowBackground();

            return window;
        }

        public static void Close(GameObject window)
        {
            WindowPool.Release(window);
            HideBackground();
        }

        public static void CloseLast()
        {
            WindowPool.ReleaseLast();
            HideBackground();
        }

        private static void ShowBackground()
        {
            if (_windowBackground != null)
                _windowBackground.gameObject.SetActive(true);
        }

        private static void HideBackground()
        {
            if (_windowBackground != null)
                _windowBackground.gameObject.SetActive(false);
        }
    }
}