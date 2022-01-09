using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleBar
{

    public class RoomDisplay : MonoBehaviour
    {
        public Sprite open;
        public Sprite closed;
        public Sprite[] roomIcons;

        public GameObject[] directions;
        public GameObject background;
        public GameObject icon;
        public GameObject playerIcon;

        public void UpdateDisplay(Room room)
        {
            playerIcon.SetActive(false);
            if (room.content == roomTypes.none)
            {
                foreach (GameObject direction in directions)
                {
                    direction.SetActive(false);
                }
                background.SetActive(false);
                icon.SetActive(false);
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (room.connections[i])
                    {
                        directions[i].GetComponent<Image>().sprite = open;
                    }
                    else
                    {
                        directions[i].GetComponent<Image>().sprite = closed;
                    }
                }
                if (room.content == roomTypes.fight)
                {
                    icon.GetComponent<Image>().sprite = roomIcons[2];
                }
                else if (room.content == roomTypes.upstairs)
                {
                    icon.GetComponent<Image>().sprite = roomIcons[0];
                }
                else if (room.content == roomTypes.downstairs)
                {
                    icon.GetComponent<Image>().sprite = roomIcons[1];
                }
                else if (room.content == roomTypes.loot)
                {
                    icon.GetComponent<Image>().sprite = roomIcons[3];
                }
                else if (room.content == roomTypes.trader)
                {
                    icon.GetComponent<Image>().sprite = roomIcons[4];
                }
            }
        }
    }

}