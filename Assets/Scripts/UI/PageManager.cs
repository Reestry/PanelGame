// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using UnityEngine;

namespace UI
{
    public class PageManager : MonoBehaviour
    {
        private static PageManager _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
                Destroy(gameObject);
            else
                _instance = this;
        }

        public static void OpenPage<T>(Page sender = null) where T : Page
        {
            if (sender != null)
                sender.Hide();

            var page = PagePool.Get<T>();

            page.transform.SetParent(_instance.transform, false);
            page.Show();
        }

        public static void ClosePage()
        {
            PagePool.Release();
        }
    }
}
