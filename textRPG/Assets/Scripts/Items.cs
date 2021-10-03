using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum itemType
{
    Food,
    Weapon
}

public static class ItemLists
{
    public static System.Type[] items = {typeof(Bread), typeof(Apple), typeof(Dagger)};
}

public class Item
{
    //Global variables
    public string name;

    public itemType type;
    public string cost;
    public bool isStackable;
    public int amount;
    public int level;
    public int quality;
    public int id;

    //Food-specific variables
    public int healthRegen;

    //Weapon-specific variables
    public int[] damage = { 0, 0 };
    public int maxDurability;
    public int currentDurability;
        
    public void init()
    {
        if (type == itemType.Food)
        {
            isStackable = true;
            amount = 1;
        }
        else
        {
            isStackable = false;
            currentDurability = maxDurability;
        }
    }
}

public class Bread : Item
{
    public Bread()
    {
        name = "Bread";
        id = 0;
        type = itemType.Food;
        healthRegen = 15;
        level = 2;
        base.init();
    }
}

public class Apple : Item
{
    public Apple()
    {
        name = "Apple";
        id = 1;
        type = itemType.Food;
        healthRegen = 5;
        level = 1;
        base.init();
    }
}

public class Dagger : Item
{
    public Dagger()
    {
        name = "Dagger";
        id = 2;
        type = itemType.Weapon;
        level = 2;
        base.init();
    }
}