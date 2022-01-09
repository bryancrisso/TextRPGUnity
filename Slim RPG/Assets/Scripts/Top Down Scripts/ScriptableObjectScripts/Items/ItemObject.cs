using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TopDown
{
    public enum ItemType
    {
        Food,
        Weapon,
        Default
    }

    public abstract class ItemObject : ScriptableObject
    {
        public GameObject prefab;
        public ItemType type;
        public string displayName;
        [TextArea(15,20)]
        public string description;

        public int cost = 0;
        public bool isStackable = false;
        public int level = 0;
        public int quality = 0;
        public int id;
    }
}