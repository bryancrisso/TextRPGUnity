using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown
{
    public class Interactor : MonoBehaviour
    {
        bool colliding = false;
        GameObject interactObj;
        public GameObject notif;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Interactable")
            {
                colliding = true;
                interactObj = collision.gameObject;
                notif.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Interactable" && interactObj == collision.gameObject)
            {
                colliding = false;
                notif.SetActive(false);
            }
        }

        public void Interact()
        {
            if (interactObj != null && colliding == true)
            {
                interactObj.GetComponent<Interactable>().Interact();
            }
        }

    }
}
