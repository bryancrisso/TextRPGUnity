using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    [CreateAssetMenu(fileName = "New Weapon Item", menuName = "InventorySystem/Items/Weapon")]
    public class WeaponItem : ItemObject
    {
        public WeaponAttack[] attackCombos;
        public float comboDelay; //the amount of time to wait before resetting a combo
        public int maxDurability = 0;
        public int currentDurability = 0;

        public void Awake()
        {
            type = ItemType.Weapon;
            currentDurability = maxDurability;
        }
    }
}