using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;

    public List<Item> inventory = new List<Item>();

    public List<Room[,]> floors = new List<Room[,]>();

    public int gold = 0;

    public string playerName = "";

    public InventoryManager inventoryUI;

    public StatManager statUI;
    public StatManager statBarUI;

    public Item currentWeapon;

    public Items items;

    private System.Random rnd = new System.Random();

    public GameManager gameManager;

    public int currentFloor = 0;
    public int[] floorCoords = { 0, 0 };

    public bool isBusy;

    // Start is called before the first frame update
    void Start()
    {
        items = GameObject.FindGameObjectWithTag("TextureManager").GetComponent<Items>();
        currentHealth = maxHealth;

        CreateFloor(currentFloor);

        inventoryAdd(0);
        inventoryAdd(2);
        inventoryAdd(2);
        inventoryAdd(1);
        inventoryAdd(1);
        currentWeapon = inventory[2];
        statUI.updateStats();
        statBarUI.updateStats();

        gameManager.floorDisplay.RoomInteraction(floors[currentFloor][floorCoords[0], floorCoords[1]]);
    }

    public void inventoryAdd(int _id)
    {
        Item item = new Item();
        item.init(items.itemList[_id]);
        if (item.isStackable == true)
        {
            bool added = false;
            foreach (Item target in inventory)
            {
                if (item.id == target.id)
                {
                    target.amount++;
                    added = true;
                }
            }
            if (!added)
            {
                inventory.Add(item);
            }
        }
        else
        {
            inventory.Add(item);
        }
        inventoryUI.updateDisplay();
    }

    public void inventoryRemove(int index)
    {
        if (inventory[index].isStackable)
        {
            inventory[index].amount--;
            if (inventory[index].amount <= 0)
            {
                inventory.Remove(inventory[index]);
            }
        }
        else
        {
            inventory.Remove(inventory[index]);
        }
        inventoryUI.updateDisplay();
    }

    public void eat(int index)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += inventory[index].healthRegen;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            inventoryRemove(index);
            statUI.updateStats();
            statBarUI.updateStats();
        }
    }
    public void equipItem(int index)
    {
        if (inventory[index].type == itemType.Weapon)
        {
            currentWeapon = inventory[index];
            statUI.updateStats();
            statBarUI.updateStats();
        }
    }
    public void damageWeapon()
    {
        if (currentWeapon != null)
        {
            int _index = inventory.IndexOf(currentWeapon);
            inventory[_index].currentDurability--;
            currentWeapon = inventory[_index];
            if (currentWeapon.currentDurability <= 0)
            {
                inventoryRemove(inventory.IndexOf(currentWeapon));
                currentWeapon = null;
            }
        }
    }
    public void attack(EnemyScript enemy)
    {
        enemy.Damage(rnd.Next(currentWeapon.damage[0], currentWeapon.damage[1]));
        damageWeapon();
        statBarUI.updateStats();
        statUI.updateStats();
    }
    public void damage(int amount)
    {
        currentHealth -= amount;
        statBarUI.updateStats();
        statUI.updateStats();
    }

    public void CreateFloor(int _floorNum)
    {
        int difficulty = _floorNum / 10 + 1;
        int baseRooms = 10;
        int extraRooms = 2;
        int currentRooms = baseRooms * difficulty + extraRooms;

        int[] roomSize = Generator.FindFloorSize(currentRooms * 2, 4, 3);
        Room[,] floorArray = Generator.Generate(roomSize[0], roomSize[1], currentRooms, gameManager.gameObject.GetComponent<Enemies>().enemyList, gameManager.gameObject.GetComponent<Items>().itemList, _floorNum);
        Generator.writeFloorToFile(floorArray);
        gameManager.floorDisplay.UpdateDisplay(floorArray);
        gameManager.floorDisplay.gameObject.GetComponent<GridLayoutGroup>().constraintCount = roomSize[0];

        floors.Add(floorArray);
    }
}
