// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    [CreateAssetMenu(fileName = "PoolConfig", menuName = "Configs/PoolConfig")]
    public class PoolConfig : ScriptableObject
    {
        public List<GameObject> Prefabs = new();
    }
}