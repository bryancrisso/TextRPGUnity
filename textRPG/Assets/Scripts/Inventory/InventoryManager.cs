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

    // Start is called before the first frame update
    void Start()
    {
        updateDisplay();
    }

    // Update is called once per frame
    void Update()
    {

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
        }
    }
    public void giveRandomItem()
    {
        int index = random.Next(ItemLists.items.Length);
        player.inventoryAdd((Item)Activator.CreateInstance(ItemLists.items[index]));
    }
}