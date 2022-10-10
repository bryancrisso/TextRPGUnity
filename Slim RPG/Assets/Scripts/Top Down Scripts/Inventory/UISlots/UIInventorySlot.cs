using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TopDown
{
    public class UIInventorySlot : MonoBehaviour
    {
        public int index;
        public InventoryManager inventoryManager;
        public ItemSelect itemSelect;
        //public GameObject functionButtons;

        //UI variables
        public TextMeshProUGUI itemName;
        //type
        //cost
        //damage
        //durability
        public TextMeshProUGUI amount;
        //quality
        public Image icon;
        //function button

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Reset()
        {
            index = -1;
            amount.text = "0";
            icon.sprite = null;
        }
        public void OnSelect(BaseEventData eventData)
        {
            if (inventoryManager != null)
            {
                inventoryManager.selectedItem = index;
            }
            if (itemSelect != null)
            {
                itemSelect.selectedItem = index;
            }
        }
    }
}
