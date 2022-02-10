using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TopDown
{
    public class InventoryManager : MonoBehaviour
    {
        public UIInventorySlot slot;
        public PlayerManager player;
        public InventoryObject inventory;
        public List<UIInventorySlot> slotList = new List<UIInventorySlot>();

        public int selectedItem = -1;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
