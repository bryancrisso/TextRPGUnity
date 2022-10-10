using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TopDown
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySystem/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        [SerializeField]
        private List<InventorySlot> Container = new List<InventorySlot>();

        public bool isEmpty()
        {
            return Container.Count == 0;
        }

        public void AddItem(ItemObject _item, int _amount)
        {
            bool hasItem = false;
            for (int i = 0; i < Container.Count; i++)
            {
                if (Container[i].item == _item)
                {
                    Container[i].addAmount(_amount);
                    hasItem = true;
                    break;
                }
            }
            if (!hasItem)
            {
                Container.Add(new InventorySlot(_item, _amount));
            }
        }

        public void RemoveItem(int index, int amount)
        {
            if (index < Container.Count)
            {
                Container[index].amount -= amount;
                if (Container[index].amount <= 0)
                {
                    Container.RemoveAt(index);
                }
            }
        }

        public InventorySlot getItem(int index)
        {
            return Container[index];
        }

        public int Size()
        {
            return Container.Count;
        }
    }

    [System.Serializable]
    public class InventorySlot
    {
        public ItemObject item;
        public int amount;

        public InventorySlot(ItemObject _item, int _amount)
        {
            item = _item;
            amount = _amount;
        }

        public void addAmount(int value)
        {
            amount += value;
        }
    }
}
