using System;
using UnityEngine;

public class GameplayManagerUI : MonoBehaviour
{
    public static GameplayManagerUI Instance;

    private void Awake()
    {
        Instance = this;
    }
}