// Copyright (c) 2012-2024 FuryLion Group. All Rights Reserved.

using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Sounds/Sound")]
public class SoundConfig: ScriptableObject
{
    [SerializeField] private SerializableDictionary<string, AudioClip> _soundDictionary = new();
        
    public SerializableDictionary<string, AudioClip> SoundDictionary => _soundDictionary;
}