using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public delegate void InteractAction();

    public InteractAction action;

    public void Interact()
    {
        action();
    }
}
