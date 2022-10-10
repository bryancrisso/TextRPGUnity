using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace TopDown
{
    public class Menu : MonoBehaviour
    {
        public bool paused = false;
        public GameObject pauseMenu, inventoryMenu, equipmentMenu;

        public GameObject pauseFirstButton, invFirstButton, invClosedButton, interactNotif, statusPanel, inventoryStream, equipFirstButton, equipClosedButton;

        public ItemSelect weaponSelect;

        public static Menu instance { get; private set; }

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }

        public void receivedPause(InputAction.CallbackContext context)
        {
            PauseUnpause();
        }

        public void PauseUnpause()
        {
            if (!paused)
            {
                paused = true;
                pauseMenu.SetActive(true);

                interactNotif.SetActive(false);
                //statusPanel.SetActive(false);
                //inventoryStream.SetActive(false);

                Time.timeScale = 0f;

                //clear selected object
                EventSystem.current.SetSelectedGameObject(null);
                //set new selected object
                EventSystem.current.SetSelectedGameObject(pauseFirstButton);
            }
            else
            {
                paused = false;
                pauseMenu.SetActive(false);
                closeInv();
                closeEquipment();

                //statusPanel.SetActive(true);
                //inventoryStream.SetActive(true);

                Time.timeScale = 1f;
            }
        }
        public IEnumerator WaitForRealSeconds(float seconds) //wait for a time that is unaffected by the timescale
        {
            float startTime = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup - startTime < seconds)
            {
                yield return null;
            }
        }

        public static IEnumerator WaitForRealSecondsStatic(float seconds) //wait for a time that is unaffected by the timescale
        {
            float startTime = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup - startTime < seconds)
            {
                yield return null;
            }
        }

        IEnumerator setInvDisplayed(bool displayed)
        {
            yield return StartCoroutine(WaitForRealSeconds(0.1f));
            inventoryMenu.GetComponent<InventoryManager>().displayed = displayed;
        }


        public void openInv()
        {
            inventoryMenu.SetActive(true);
            StartCoroutine("setInvDisplayed", true); //delay inventory set as displayed to prevent instant usage of items

            //clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            //set new selected object
            EventSystem.current.SetSelectedGameObject(invFirstButton);
        }

        public void closeInv()
        {
            inventoryMenu.GetComponent<InventoryManager>().displayed = false;
            inventoryMenu.SetActive(false);

            //clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            //set new selected object
            EventSystem.current.SetSelectedGameObject(invClosedButton);
        }

        public void openEquipment()
        {
            equipmentMenu.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            //set to button that should be first selected
            EventSystem.current.SetSelectedGameObject(equipFirstButton);
        }

        public void closeEquipment()
        {
            equipmentMenu.SetActive(false);

            EventSystem.current.SetSelectedGameObject(null);
            //set to the equipment button on the menu
            EventSystem.current.SetSelectedGameObject(equipClosedButton);
        }

        public async void selectWeapon()
        {
            InventorySlot slot = await weaponSelect.getItem();
            Debug.Log(slot.item.name);
        }
    }
}
