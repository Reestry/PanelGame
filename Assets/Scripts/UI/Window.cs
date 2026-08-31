// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class Window : MonoBehaviour
    {
        public void Show()
        {
            var defaultScale = transform.localScale;
            gameObject.SetActive(true);
            
            transform.localScale = Vector3.zero;

            transform.DOScale(defaultScale, 0.6f).SetEase(Ease.OutFlash).SetAutoKill(true);

        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
