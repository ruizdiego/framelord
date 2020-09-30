// Unity Framework
using UnityEngine;

namespace FrameLord.Pool
{
    public class PoolItem : MonoBehaviour
    {
        // Pool Manager reference
        private Pool _poolManager;

        /// <summary>
        /// Set the pool manager reference
        /// </summary>
        public void SetPoolManager(Pool pm)
        {
            _poolManager = pm;
        }

        /// <summary>
        /// Return the item to the pool
        /// </summary>
        public void ReturnToPool()
        {
            _poolManager.ReturnItem(this);
        }

    }
}