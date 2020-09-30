// .NET Framework
using System.Collections.Generic;

// Unity Framework
using UnityEngine;

namespace FrameLord.Pool
{
    public class Pool : MonoBehaviour
    {
        // Pool name
        public string poolName;

        // Reference to the prefab of the item to create
        public GameObject poolItemPrefab;

        // Number of items to precreate
        public int itemsToPreCreate = 10;

        // List of items created and ready to use
        private List<PoolItem> _itemList;

        /// <summary>
        /// Unity Start Method
        /// </summary>
        void Start()
        {
            CreateItems();
        }

        /// <summary>
        /// Pre-Create the items and deactivate them
        /// </summary>
        private void CreateItems()
        {
            _itemList = new List<PoolItem>(itemsToPreCreate);

            for (int i = 0; i < itemsToPreCreate; i++)
            {
                var go = GameObject.Instantiate(poolItemPrefab, Vector3.zero, Quaternion.identity, transform);
                go.name = $"{poolItemPrefab.name}-{i}";
                go.GetComponent<PoolItem>().SetPoolManager(this);
                _itemList.Add(go.GetComponent<PoolItem>());
                go.SetActive(false);
            }
        }

        /// <summary>
        /// Get the item
        /// </summary>
        public PoolItem GetItem()
        {
            if (_itemList.Count > 0)
            {
                var projectile = _itemList[0];
                _itemList.RemoveAt(0);
                return projectile;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Return the specified item
        /// </summary>
        public void ReturnItem(PoolItem item)
        {
            item.gameObject.SetActive(false);
            _itemList.Add(item);
        }
    }
}