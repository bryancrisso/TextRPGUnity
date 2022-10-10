using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

namespace TopDown
{
    public class InventoryManager : MonoBehaviour
    {
        public bool displayed;
        public PlayerManager player;
        private InventoryObject inventory;
        public List<UIInventorySlot> slotList = new List<UIInventorySlot>();
        public InventoryStream inventoryStream;

        public static InventoryManager instance { get; private set; }

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
            // If there is an instance, and it's not me, delete myself.

            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            inventory = player.inventory;
            recalcViewRange();
            foreach (UIInventorySlot slot in slotList)
            {
                slot.inventoryManager = this;
            }
            displayInventory();
            gameObject.SetActive(false);
        }

        public void AddItem(ItemObject item, int amount)
        {
            inventory.AddItem(item, amount);

            recalcViewRange();

            inventoryStream.addToStream(item, amount);
            displayInventory();
        }

        public void RemoveItem(int index, int amount)
        {
            inventory.RemoveItem(index, amount);

            recalcViewRange();
            
            displayInventory();
        }

        private void recalcViewRange()
        {
            if (page != 0 && page*16>=inventory.Size())
            {
                page--;
            }
            viewRange = new int[] { page * 16, Mathf.Min(inventory.Size() - 1, (page + 1) * 16 - 1) };
        }

        public void useSelectedItem(InputAction.CallbackContext context)
        {
            if (displayed && selectedItem != -1 && context.performed)
            {
                if (inventory.getItem(selectedItem).item.type == ItemType.Food)
                {
                    player.GetComponent<HealthManager>().Heal(((FoodItem)inventory.getItem(selectedItem).item).healthRegen);
                    RemoveItem(selectedItem, 1);
                }
            }
        }

        void updateSelection()
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
