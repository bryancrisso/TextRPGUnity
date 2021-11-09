using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject slot;
    public Player player;
    public List<GameObject> slotList = new List<GameObject>();

    System.Random random = new System.Random();

    public int selectedIndex = -1;
    public SelectedItem selectWindow;
    public Items items;

    // Start is called before the first frame update
    void Start()
    {
        updateDisplay();
        items = GameObject.FindGameObjectWithTag("TextureManager").GetComponent<Items>();
    }

    public void updateDisplay()
    {
        foreach (GameObject item in slotList)
        {
            Destroy(item);
        }
        slotList.Clear();
        for (int i = 0; i < player.inventory.Count; i++)
        {
            GameObject slotInstance = Instantiate(slot, new Vector3(0, 0, 0), Quaternion.identity);
            slotList.Add(slotInstance);
            slotInstance.transform.SetParent(transform);
            slotInstance.GetComponent<InventorySlot>().index = i;
            slotInstance.GetComponent<InventorySlot>().player = player;
            slotInstance.GetComponent<InventorySlot>().updateData();
            slotInstance.transform.localScale = new Vector3(1, 1, 1);
        }
        selectWindow.UpdateDisplay(-1);
        selectedIndex = -1;
    }

    public void giveRandomItem()
    {
        int index = random.Next(items.itemList.Count);
        player.inventoryAdd(index);
    }

    public void updateSelectedItem(int index)
    {
        if (selectedIndex != -1) //disable past selected item
        {
            InventorySlot pastSlot = slotList[selectedIndex].GetComponent<InventorySlot>();
            pastSlot.updateData();
            pastSlot.info.SetActive(false);
            pastSlot.infoActive = false;
        }
        if (index != -1) //enable currently selected item
        {
            InventorySlot currentSlot = slotList[index].GetComponent<InventorySlot>();
            currentSlot.updateData();
            currentSlot.info.SetActive(true);
            currentSlot.infoActive = true;
            selectWindow.UpdateDisplay(index);
        }
        else
        {
            selectWindow.UpdateDisplay(-1);
        }
        selectedIndex = index;
    }
}