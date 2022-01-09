using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BattleBar
{

    public class SelectedItem : MonoBehaviour
    {
        public GameObject itemDisplay;
        public Image icon;
        public TextMeshProUGUI itemName;
        public TextMeshProUGUI itemType;
        public TextMeshProUGUI itemCost;

        public TextMeshProUGUI itemDurability;
        public TextMeshProUGUI itemDamage;
        public TextMeshProUGUI itemAmount;
        public TextMeshProUGUI itemQuality;

        public GameObject functionButton;
        public TextMeshProUGUI functionButtonText;

        public int selectedIndex;

        public InventoryManager inventoryManager;

        public void UpdateDisplay(int index)
        {
            if (index == -1)
            {
                itemDisplay.SetActive(false);
                itemName.text = "";
                itemType.text = "";
                itemCost.text = "";
                itemDurability.text = "";
                itemDamage.text = "";
                itemQuality.text = "";
                functionButton.SetActive(false);
            }
            else
            {
                itemDisplay.SetActive(true);
                InventorySlot slot = inventoryManager.slotList[index].GetComponent<InventorySlot>();
                icon.sprite = slot.icon.sprite;
                itemName.text = slot.name.text;
                itemType.text = slot.type.text;
                itemCost.text = slot.cost.text;
                itemDurability.text = slot.durability.text;
                itemDamage.text = slot.damage.text;
                itemAmount.text = slot.amount.text;
                itemQuality.text = slot.quality.text;
                functionButton.SetActive(true);
                functionButtonText.text = slot.functionButton.text;
            }
            selectedIndex = index;
        }

        private void Start()
        {
            UpdateDisplay(-1);
        }

        public void functionClick()
        {
            inventoryManager.slotList[selectedIndex].GetComponent<InventorySlot>().functionClick();
        }
    }

}