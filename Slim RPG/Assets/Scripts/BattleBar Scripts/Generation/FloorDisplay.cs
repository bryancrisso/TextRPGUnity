using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleBar
{

    public class FloorDisplay : MonoBehaviour
    {
        public GameObject roomPrefab;

        public int xSize;
        public int ySize;

        private Room[,] floor;

        private List<GameObject> roomDisplayObjects = new List<GameObject>();

        public GameManager gameManager;

        public GameObject upButton;
        public GameObject downButton;

        public void UpdateDisplay(Room[,] array)
        {
            GameObject roomGO;
            if (roomDisplayObjects.Count > 0)
            {
                foreach (GameObject room in roomDisplayObjects)
                {
                    Destroy(room);
                }
            }
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    roomGO = Instantiate(roomPrefab, new Vector3(50 + j * 40, -50 - i * 40, 0), Quaternion.identity, this.transform);
                    roomGO.GetComponent<RoomDisplay>().UpdateDisplay(array[i, j]);
                    roomDisplayObjects.Add(roomGO);
                    if (i == gameManager.player.floorCoords[0] && j == gameManager.player.floorCoords[1])
                    {
                        roomGO.GetComponent<RoomDisplay>().playerIcon.SetActive(true);
                    }
                }
            }
        }


        public void MovePlayer(int direction)
        {
            if (!gameManager.player.isBusy)
            {
                if (direction == 1 && gameManager.player.floors[gameManager.player.currentFloor][gameManager.player.floorCoords[0], gameManager.player.floorCoords[1]].connections[0])
                {
                    if (gameManager.player.floorCoords[0] > 0)
                    {
                        gameManager.player.floorCoords[0] -= 1;
                    }
                }
                else if (direction == 2 && gameManager.player.floors[gameManager.player.currentFloor][gameManager.player.floorCoords[0], gameManager.player.floorCoords[1]].connections[1])
                {
                    if (gameManager.player.floorCoords[1] < gameManager.player.floors[gameManager.player.currentFloor].GetLength(1) - 1)
                    {
                        gameManager.player.floorCoords[1] += 1;
                    }
                }
                else if (direction == 3 && gameManager.player.floors[gameManager.player.currentFloor][gameManager.player.floorCoords[0], gameManager.player.floorCoords[1]].connections[2])
                {
                    if (gameManager.player.floorCoords[0] < gameManager.player.floors[gameManager.player.currentFloor].GetLength(0) - 1)
                    {
                        gameManager.player.floorCoords[0] += 1;
                    }
                }
                else if (direction == 4 && gameManager.player.floors[gameManager.player.currentFloor][gameManager.player.floorCoords[0], gameManager.player.floorCoords[1]].connections[3])
                {
                    if (gameManager.player.floorCoords[1] > 0)
                    {
                        gameManager.player.floorCoords[1] -= 1;
                    }
                }
                gameManager.lootDisplay.gameObject.SetActive(false);
                UpdateDisplay(gameManager.player.floors[gameManager.player.currentFloor]);
                RoomInteraction(gameManager.player.floors[gameManager.player.currentFloor][gameManager.player.floorCoords[0], gameManager.player.floorCoords[1]]);
            }
        }

        public void RoomInteraction(Room room)
        {
            if (!room.explored)
            {
                upButton.SetActive(false);
                downButton.SetActive(false);
                if (room.content == roomTypes.fight)
                {
                    gameManager.player.isBusy = true;
                    gameManager.startButton.SetActive(true);
                }
                else if (room.content == roomTypes.loot)
                {
                    Sprite _foodIcon;
                    Sprite _weaponIcon;

                    gameManager.lootDisplay.gameObject.SetActive(true);
                    gameManager.player.isBusy = true;
                    gameManager.player.inventoryAdd(room.loot[0].id);
                    if (room.loot[0].id < gameManager.tManager.textures.Length)
                    {
                        _foodIcon = gameManager.tManager.textures[room.loot[0].id];
                    }
                    else
                    {
                        _foodIcon = gameManager.tManager.nullTexture;
                    }

                    if (room.loot.Count > 1)
                    {
                        gameManager.player.inventoryAdd(room.loot[1].id);
                        if (room.loot[1].id < gameManager.tManager.textures.Length)
                        {
                            _weaponIcon = gameManager.tManager.textures[room.loot[1].id];
                        }
                        else
                        {
                            _weaponIcon = gameManager.tManager.nullTexture;
                        }
                        gameManager.lootDisplay.DisplayLoot(_foodIcon, _weaponIcon);
                    }
                    else
                    {
                        gameManager.lootDisplay.DisplayLoot(_foodIcon);
                    }
                    gameManager.player.isBusy = false;
                }
                room.explored = true;
            }
            if (room.content == roomTypes.upstairs)
            {
                upButton.SetActive(true);
                downButton.SetActive(false);
            }
            if (room.content == roomTypes.downstairs)
            {
                upButton.SetActive(false);
                downButton.SetActive(true);
            }
        }

        /// <summary>
        /// Moves the player up or down a floor
        /// </summary>
        /// <param name="direction">0 for down, 1 for up</param>
        public void ChangeFloor(int direction)
        {
            if (direction == 1)
            {
                if (gameManager.player.currentFloor + 1 == gameManager.player.floors.Count)
                {
                    gameManager.player.currentFloor++;
                    gameManager.player.CreateFloor(gameManager.player.currentFloor);
                    gameManager.player.floorCoords = new int[] { 0, 0 };
                }
                else
                {
                    gameManager.player.currentFloor++;
                    gameManager.player.floorCoords = new int[] { 0, 0 };
                    UpdateDisplay(gameManager.player.floors[gameManager.player.currentFloor]);
                }
            }
            else if (direction == 0)
            {
                if (gameManager.player.currentFloor > 0)
                {
                    gameManager.player.currentFloor--;
                    gameManager.player.floorCoords = new int[] { 0, 0 };
                    UpdateDisplay(gameManager.player.floors[gameManager.player.currentFloor]);
                }
            }
        }
    }

}