using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctions : MonoBehaviour
{
    private bool toggled = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(toggled);
    }

    public void toggleInventory()
    {
        toggled = !toggled;
        gameObject.SetActive(toggled);
    }
}
