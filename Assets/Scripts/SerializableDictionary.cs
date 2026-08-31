// Copyright (c) 2012-2024 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;

[System.Serializable]
public class SerializableDictionary<TKey, TValue>
{
    public List<TKey> Keys = new();
    public List<TValue> Values = new();
    
    public void Add(TKey key, TValue value)
    {
        Keys.Add(key);
        Values.Add(value);
    }

    public bool ContainsKey(TKey key)
    {
        return Keys.Contains(key);
    }

    public TValue this[TKey key]
    {
        get => Values[Keys.IndexOf(key)];
        set
        {
            if (Keys.Contains(key))
                Values[Keys.IndexOf(key)] = value;
            else
            {
                Keys.Add(key);
                Values.Add(value);
            }
        }
    }
}