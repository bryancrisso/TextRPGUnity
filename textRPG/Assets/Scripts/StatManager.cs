using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatManager : MonoBehaviour
{
    public Player player;

    //UI Variables
    public TextMeshProUGUI healthText;
    public Slider healthSlider;
    public TextMeshProUGUI goldText;
    public InventorySlot currentWeapon;

    public void updateStats()
    {
        healthText.text = player.currentHealth.ToString() + "/" + player.maxHealth.ToString();
        healthSlider.value = player.currentHealth;
        healthSlider.maxValue = player.maxHealth;
        goldText.text = player.gold.ToString();
        currentWeapon.index = player.inventory.IndexOf(player.currentWeapon);
        currentWeapon.updateData();
    }
}
