using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    public int index = 0;
    public GameObject info;
    public bool infoActive = false;
    public InventoryManager inventoryManager;

    //UI variables
    public TextMeshProUGUI name;
    public TextMeshProUGUI type;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI durability;
    public TextMeshProUGUI amount;
    public TextMeshProUGUI quality;
    public Image icon;
    public TextMeshProUGUI functionButton;

    public Player player;
    public TextureManager tManager;

    //Internal Info Variables
    private bool isOverLast;

    // Start is called before the first frame update
    void Awake()
    {
        info.SetActive(infoActive);
        tManager = GameObject.FindWithTag("TextureManager").GetComponent<TextureManager>();
        inventoryManager = GameObject.FindWithTag("TextureManager").GetComponent<GameManager>().invManager;
    }

    public void onClick()
    {
        if (!infoActive)
        {
            inventoryManager.updateSelectedItem(index);
        }
        else
        {
            inventoryManager.updateSelectedItem(-1);
        }
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
            switch (item.quality)
            {
                case 1:
                    quality.text = "Quality: Common";
                    break;
                case 2:
                    quality.text = "Quality: Rare";
                    break;
                case 3:
                    quality.text = "Quality: Epic";
                    break;
                case 4:
                    quality.text = "Quality: Legendary";
                    break;
            }
        }
        else if (item.type == itemType.Food)
        {
            type.text = "Level " + item.level + " Food";
            damage.text = "Heals " + item.healthRegen + " Health";
            durability.text = "";
            amount.text = item.amount.ToString();
            functionButton.text = "Eat";
            switch (item.quality)
            {
                case 1:
                    quality.text = "Quality: Common";
                    break;
                case 2:
                    quality.text = "Quality: Rare";
                    break;
                case 3:
                    quality.text = "Quality: Epic";
                    break;
                case 4:
                    quality.text = "Quality: Legendary";
                    break;
            }
        }
        if (item.id < tManager.textures.Length)
        {
            icon.sprite = tManager.textures[item.id];
        }
        else
        {
            icon.sprite = tManager.nullTexture;
        }
    }
}
