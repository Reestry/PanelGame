// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class WindowPool : MonoBehaviour
    {
        [SerializeField] private List<Window> _prefabs;

        private static WindowPool _instance;
        private readonly List<Window> _windows = new();
        public static readonly Stack<Window> OpenedWindows = new();

        private void Awake()
        {
            if (_instance != null && _instance != this)
                Destroy(gameObject);
            else
                _instance = this;

            _windows.Clear();
            InitPrefabs();
        }

        private void InitPrefabs()
        {
            foreach (var windowPrefab in _prefabs)
            {
                var window = Instantiate(windowPrefab, transform);
                window.gameObject.SetActive(false);
                _windows.Add(window);
            }
        }

        public static T Get<T>() where T : Window
        {
            foreach (var window in _instance._windows)
            {
                if (window is not T typedObj)
                    continue;
                
                OpenedWindows.Push(typedObj);
                return typedObj;
            }

            return null;
        }

        public static void Release(GameObject window)
        {
            if (OpenedWindows.Count <= 0)
                return;

            window.SetActive(false);
            window.transform.SetParent(_instance.transform, false);
        }

        public static void ReleaseLast()
        {
            if (OpenedWindows.Count <= 0)
                return;

            var window = OpenedWindows.Pop().gameObject;
            window.SetActive(false);
            window.transform.SetParent(_instance.transform, false);
        }
    }
}