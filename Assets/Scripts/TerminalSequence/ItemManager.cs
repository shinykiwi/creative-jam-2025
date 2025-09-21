using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TerminalSequence
{
    public class ItemManager : MonoBehaviour
    {
        [SerializeField] private ItemSO[] startingItems;
        private List<ItemSO> items;
        public static ItemManager Instance { get; private set; }
        private void Awake() 
        { 
            // If there is an instance, and it's not me, delete myself.
    
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }

        private void Start()
        {
            items = new List<ItemSO>();
            Debug.Log(items);
            foreach (var item in startingItems)
            {
               AddItem(item); 
            }
        }

        public void AddItem(ItemSO item)
        {
            items.Add(item);
        }

        public void RemoveItem(int id)
        {
            ItemSO itemToBeRemoved = null;

            foreach (var item in items.Where(item => item.id == id))
            {
                itemToBeRemoved = item;
            }

            items.Remove(itemToBeRemoved);
        }

        public void Clear()
        {
            items.Clear();
        }

        public int GetItemCount()
        {
            Debug.Log(items.Count);
            return items.Count;
        }
    }
}
