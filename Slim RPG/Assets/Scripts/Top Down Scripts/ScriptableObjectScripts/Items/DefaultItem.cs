using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TopDown
{
    [CreateAssetMenu(fileName = "New Default Item", menuName = "InventorySystem/Items/Default")]
    public class DefaultItem : ItemObject
    {
        public void Awake()
        {
            type = ItemType.Default;
        }
    }
}
