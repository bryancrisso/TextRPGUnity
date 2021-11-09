using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    public List<string> saves = new List<string>();

    public GameObject saveList;

    public GameObject savePanel;

    public void LoadSave(string saveFolder)
    {
        string playerJson = File.ReadAllText(saveFolder + "\\player.json");
        Player _player = JsonConvert.DeserializeObject<Player>(playerJson);

        Player player = gameObject.GetComponent<GameManager>().player;

        player.currentHealth = _player.currentHealth;
        player.maxHealth = _player.maxHealth;

        player.inventory = _player.inventory;
        player.floors = _player.floors;

        player.gold = _player.gold;

        player.playerName = _player.playerName;

        player.currentWeapon = _player.currentWeapon;

        player.currentFloor = _player.currentFloor;
        player.floorCoords = _player.floorCoords;

        player.statUI.updateStats();
        player.statBarUI.updateStats();

        player.gameManager.floorDisplay.RoomInteraction(player.floors[player.currentFloor][player.floorCoords[0], player.floorCoords[1]]);
    }

    public void CreateSave(string saveFolder)
    {
        saves.Add(saveFolder);
        Directory.CreateDirectory(saveFolder);
        Save(saveFolder);
        GameObject _savePanel = Instantiate(savePanel, saveList.transform);
        _savePanel.GetComponent<SavePanel>().index = saves.Count;
        _savePanel.GetComponent<SavePanel>().saveNameText.text = saveFolder;

    }

    public void Save(string saveFolder)
    {
        string playerJson = JsonConvert.SerializeObject(gameObject.GetComponent<GameManager>().player).ToString();
        File.WriteAllText(saveFolder + "\\player.json", playerJson);
    }
}