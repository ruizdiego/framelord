// .NET Framework
using System.Collections.Generic;

// Unity Framework
using UnityEngine;

// FrameLord
using FrameLord.Core;

namespace FrameLord.Pool
{
    public class PoolManager : MonoBehaviorSingleton<PoolManager>
    {
        private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

        protected override void Initialize()
        {
            var trnf = transform;
            trnf.position = Vector3.zero;
            trnf.rotation = Quaternion.identity;
            trnf.localScale = Vector3.one;
        }

        /// <summary>
        /// Unity Awake Method
        /// </summary>
        new void Awake()
        {
            base.Awake();

            var poolComponents = GetComponents<Pool>();
            for (int i = 0; i < poolComponents.Length; i++)
            {
                pools.Add(poolComponents[i].poolName, poolComponents[i]);
            }
        }

        /// <summary>
        /// Register a pool to the pool manager
        /// </summary>
        public void RegisterPool(string poolName, Pool pool)
        {
            if (pools == null) pools = new Dictionary<string, Pool>();
            if (!pools.ContainsKey(poolName)) pools.Add(poolName, pool);
        }

        /// <summary>
        /// Unregister a pool to the pool manager
        /// </summary>
        public void UnregisterPool(string poolName)
        {
            if (pools != null && pools.ContainsKey(poolName))
            {
                pools.Remove(poolName);
            }
        }

        /// <summary>
        /// Returns the reference to the specified pool
        /// </summary>
        public Pool GetPool(string poolName)
        {
            return pools[poolName];
        }
    }
}