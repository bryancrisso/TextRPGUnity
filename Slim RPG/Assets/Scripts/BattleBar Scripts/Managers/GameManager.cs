using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BattleBar
{
    public class GameManager : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public BattleBar battleBar;
        public Player player;
        public InventoryManager invManager;
        public TextureManager tManager;

        private GameObject enemyGO;

        public GameObject startButton;

        public LootDisplay lootDisplay;

        public FloorDisplay floorDisplay;

        private System.Random gen = new System.Random();

        public void testBattle()
        {
            Enemy _enemy = new Enemy();
            _enemy.init(player.floors[player.currentFloor][player.floorCoords[0], player.floorCoords[1]].enemy);
            StartBattle(_enemy, player);
        }

        public void OnStartBattleClick(Room room)
        {
            Enemy _enemy = new Enemy();
            _enemy.init(room.enemy);
            StartBattle(_enemy, player);
        }

        public void StartBattle(Enemy enemy, Player player)
        {
            enemyGO = Instantiate(enemyPrefab);
            enemyGO.GetComponent<EnemyScript>().Initialise(enemy);

            battleBar.gameManager = this;
            battleBar.player = player;
            battleBar.enemy = enemyGO.GetComponent<EnemyScript>();

            battleBar.gameObject.SetActive(true);
            battleBar.isFighting = true;

            battleBar.ClearTriggers();
            battleBar.cursor.transform.localPosition = new Vector2(battleBar.boundaries[0], battleBar.cursor.transform.localPosition.y);

            player.isBusy = true;
        }

        public void endBattle(bool hasWon)
        {
            if (hasWon)
            {
                battleBar.isFighting = false;
                battleBar.gameObject.SetActive(false);

                player.gold += gen.Next(enemyGO.GetComponent<EnemyScript>().correspondingEnemy.gold[0], enemyGO.GetComponent<EnemyScript>().correspondingEnemy.gold[1]);

                player.statUI.updateStats();
                player.statBarUI.updateStats();

                Destroy(enemyGO);

                startButton.SetActive(false);

                player.isBusy = false;
            }
            else
            {
                battleBar.isFighting = false;
                battleBar.gameObject.SetActive(false);

                Destroy(enemyGO);

                startButton.SetActive(false);

                player.isBusy = false;

                player.currentHealth = 100;
                player.floorCoords = new int[] { 0, 0 };
                player.currentFloor = 0;
                player.gold /= 2;
                floorDisplay.UpdateDisplay(player.floors[0]);

                player.statUI.updateStats();
                player.statBarUI.updateStats();
            }
        }
    }

}
