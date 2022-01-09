using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    [CreateAssetMenu(fileName = "New Food Item", menuName = "InventorySystem/Items/Food")]
    public class FoodItem : ItemObject
    {
        public int healthRegen;

        public void Awake()
        {
            type = ItemType.Food;
        }
    }
}