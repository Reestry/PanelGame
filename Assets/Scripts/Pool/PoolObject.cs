// Copyright (c) 2012-2023 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class PoolObject : MonoBehaviour
    {
        [SerializeField] private PoolConfig _poolConfig;

        private static PoolObject _instance;

        private static IEnumerable<GameObject> Items => _instance._poolConfig.Prefabs;

        private static readonly List<MonoBehaviour> PooledObjects = new();

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
        }

        public static T Get<T>(Transform parent = null, GameObject prefab = null) where T : MonoBehaviour
        {
            var obj = PooledObjects.Find(o => o.GetType() == typeof(T)) as T;
            if (obj == null)
            {
                if (prefab != null)
                    obj = Instantiate(prefab, parent).GetComponent<T>();
                else
                {
                    foreach (var item in Items)
                    {
                        if (item.TryGetComponent(out T value))
                        {
                            obj = Instantiate(value, parent);
                            break;
                        }
                    }
                }

                if (obj != null)
                {
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }
            else
            {
                obj.gameObject.SetActive(true);
                obj.transform.SetParent(parent);
                PooledObjects.Remove(obj);
            }

            return obj;
        }

        public static void Release<T>(T obj) where T : MonoBehaviour
        {
            if (obj is IReleasable releasable)
                releasable.OnRelease();

            obj.gameObject.SetActive(false);
            obj.transform.SetParent(null);
            obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            PooledObjects.Add(obj);
        }
    }
}