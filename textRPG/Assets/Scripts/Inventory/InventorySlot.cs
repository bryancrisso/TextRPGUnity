using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public int index;
    public GameObject info;
    private bool infoActive;

    //UI variables
    public TextMeshProUGUI name;
    public TextMeshProUGUI type;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI durability;
    public TextMeshProUGUI amount;
    public Image icon;
    public TextMeshProUGUI functionButton;

    public Player player;
    public TextureManager tManager;

    // Start is called before the first frame update
    void Start()
    {
        info.SetActive(false);
        infoActive = false;
    }

    void Awake()
    {
        tManager = FindObjectOfType<TextureManager>();
    }

    public void onClick()
    {
        updateData();
        info.SetActive(!infoActive);
        infoActive = !infoActive;
    }

    public void functionClick()
    {
        Item item = player.inventory[index];
        if (item.type == itemType.Weapon)
        {
            player.equipItem(index);
        }
        else if (item.type == itemType.Food)
        {
            player.eat(index);
        }
    }

    public void updateData()
    {
        Item item = player.inventory[index];
        name.text = item.name;
        cost.text = "Cost: " + item.cost + " Gold";
        if (item.type == itemType.Weapon)
        {
            type.text = "Level " + item.level + " Weapon";
            damage.text = "Deals (" + item.damage[0] + "," + item.damage[1] + ") Damage";
            durability.text = "Durability: " + item.currentDurability + "/" + item.maxDurability;
            amount.text = "";
            functionButton.text = "Equip";
        }
        else if (item.type == itemType.Food)
        {
            type.text = "Level " + item.level + " Food";
            damage.text = "Heals " + item.healthRegen + " Health";
            durability.text = "";
            amount.text = item.amount.ToString();
            functionButton.text = "Eat";
        }
        icon.sprite = tManager.textures[item.id];
    }
}
