using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctions : MonoBehaviour
{
    private bool inventoryToggled = true;
    private bool floorToggled = false;
    public GameObject statBar;
    public GameObject floorDisplay;
    public GameManager gameManager;

    void Start()
    {
        gameObject.SetActive(inventoryToggled);
        statBar.SetActive(!inventoryToggled);
        floorDisplay.SetActive(!inventoryToggled);
    }

    public void toggleInventory()
    {
        inventoryToggled = !inventoryToggled;
        floorToggled = false;

        gameObject.SetActive(inventoryToggled);
        statBar.SetActive(!inventoryToggled);
        floorDisplay.SetActive(false);
    }

    public void toggleFloorDisplay()
    {
        inventoryToggled = false;
        floorToggled = !floorToggled;

        gameObject.SetActive(false);
        statBar.SetActive(!floorToggled);
        floorDisplay.SetActive(floorToggled);

    }
}
