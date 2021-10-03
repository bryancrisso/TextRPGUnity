using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;

    //private var currentWeapon;

    public List<Item> inventory = new List<Item>();

    public int gold = 0;

    public string playerName = "";

    public int currentFloor = 1;

    public InventoryManager inventoryUI;

    public StatManager statUI;

    public Item currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth - 20;
        inventoryAdd(new Bread());
        inventoryAdd(new Apple());
        inventoryAdd(new Apple());
        inventoryAdd(new Dagger());
        inventoryAdd(new Dagger());
        statUI.updateStats();
    }

    public void inventoryAdd(Item item)
    {
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
        } else
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
        }
    }
    public void equipItem(int index)
    {
        if (inventory[index].type == itemType.Weapon)
        {
            currentWeapon = inventory[index];
            statUI.updateStats();
        }
    }
    public void damageWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon.currentDurability--;
            if (currentWeapon.currentDurability >= 0)
            {
                inventoryRemove(inventory.IndexOf(currentWeapon));
                currentWeapon = null;
            }
        }
    }
}
