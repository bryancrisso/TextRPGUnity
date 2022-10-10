using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

namespace TopDown
{
    public class ItemSelect : MonoBehaviour
    {
        public bool displayed = false;
        public PlayerManager player;
        private InventoryObject inventory;
        public List<UIInventorySlot> slotList = new List<UIInventorySlot>();
        private InventoryManager inventoryManager;
        public ItemType selectType;
        public GameObject buttonSelect;

        public bool hasSelected = false;

        //update the selection each time it changes
        private int _selectedItem;
        public int selectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                updateSelection();
            }
        }

        public int[] viewRange;
        public int page = 0;

        [Header("SelectedWindow")]
        public Image selectedIcon;
        public TextMeshProUGUI selectedName;
        public TextMeshProUGUI selectedInfo;
        public TextMeshProUGUI foodInfo;

        private void Awake()
        {
            inventoryManager = InventoryManager.instance;
            inventory = player.inventory;
            recalcViewRange();
            foreach (UIInventorySlot slot in slotList)
            {
                slot.itemSelect = this;
            }
            displayInventory();
            //gameObject.SetActive(false);
        }

        private void recalcViewRange()
        {
            if (page != 0 && page * 16 >= inventory.Size())
            {
                page--;
            }
            viewRange = new int[] { page * 16, Mathf.Min(inventory.Size() - 1, (page + 1) * 16 - 1) };
        }

        void updateSelection() //updates the selected item window
        {
            if (selectedItem != -1)
            {
                try
                {
                    selectedIcon.sprite = inventory.getItem(selectedItem).item.displayImage;
                    selectedName.text = inventory.getItem(selectedItem).item.displayName;
                    selectedInfo.text = inventory.getItem(selectedItem).item.description;
                    if (inventory.getItem(selectedItem).item.type == ItemType.Food)
                    {
                        foodInfo.gameObject.SetActive(true);
                        foodInfo.text = "Interact: Heal for " + ((FoodItem)inventory.getItem(selectedItem).item).healthRegen.ToString();
                    }
                    else
                    {
                        foodInfo.gameObject.SetActive(false);
                    }
                }
                catch
                {
                    waiter(0.1f);
                }
            }
        }

        public void selectItem(InputAction.CallbackContext context)
        {
            if (displayed && selectedItem != -1 && context.performed)
            {
                if (inventory.getItem(selectedItem).item.type == selectType)
                {
                    Debug.Log("Selected!");
                    hasSelected = true;
                }
            }
        }


        public async Task<InventorySlot> getItem()
        {
            gameObject.SetActive(true);
            displayed = true;

            GameObject prevselect = EventSystem.current.currentSelectedGameObject;
            EventSystem.current.SetSelectedGameObject(buttonSelect);

            while (!hasSelected)
            {
                Debug.Log("Waiting...");
                await Task.Delay(100);
            }

            EventSystem.current.SetSelectedGameObject(prevselect);
            hasSelected = false;
            displayed = false;
            gameObject.SetActive(false);

            return inventory.getItem(selectedItem);
        }

        //waits for a certain amount of time before updating the selected item window
        IEnumerator waiter(float timeToWait)
        {
            yield return new WaitForSeconds(timeToWait);
            selectedIcon.sprite = inventory.getItem(selectedItem).item.displayImage;
            selectedName.text = inventory.getItem(selectedItem).item.displayName;
            selectedInfo.text = inventory.getItem(selectedItem).item.description;
        }

        void displayInventory()
        {
            updateSelection();
            if (!inventory.isEmpty())
            {
                //reset slots
                foreach (UIInventorySlot slot in slotList)
                {
                    slot.Reset();
                }

                for (int i = viewRange[0]; i <= viewRange[1]; i++)
                {
                    //i is the index in the inventory
                    //j is the visual slot index
                    int j = i - viewRange[0];
                    slotList[j].index = i;
                    slotList[j].amount.text = inventory.getItem(i).amount.ToString();
                    slotList[j].icon.sprite = inventory.getItem(i).item.displayImage;
                }
            }
            else
            {
                selectedItem = -1;
                //reset slots
                foreach (UIInventorySlot slot in slotList)
                {
                    slot.Reset();
                }
            }
        }

    }
}
