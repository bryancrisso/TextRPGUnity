using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerTypes
{
    Weapon,
    Enemy,
    Heal
}

public class BattleBar : MonoBehaviour
{
    public GameObject cursor;
    public int[] boundaries;
    public int cursorSpeed;
    public int enemyTriggerSpeed;

    public bool isFighting = false;
    private bool direction = true;

    public GameObject weaponTrigger;
    public GameObject enemyTrigger;

    public Player player;
    public GameManager gameManager;
    public EnemyScript enemy;

    public float weaponTriggerTimer = 2;
    public float enemyTriggerTimer = 3;

    private System.Random rnd;

    private List<GameObject> weaponTriggers = new List<GameObject>();
    private List<GameObject> enemyTriggers = new List<GameObject>();

    public Transform weaponTriggerParent;

    private void Start()
    {
        rnd = new System.Random();
    }

    private void Update()
    {
        if (isFighting)
        {
            //Moving the Cursor
            if (direction)
            {
                cursor.transform.localPosition = Vector2.Lerp(cursor.transform.localPosition, new Vector2(cursor.transform.localPosition.x + cursorSpeed, cursor.transform.localPosition.y), Time.deltaTime);
            }
            else
            {
                cursor.transform.localPosition = Vector2.Lerp(cursor.transform.localPosition, new Vector2(cursor.transform.localPosition.x - cursorSpeed, cursor.transform.localPosition.y), Time.deltaTime);
            }

            if (cursor.transform.localPosition.x > boundaries[1])
            {
                direction = false;
            }
            else if (cursor.transform.localPosition.x < boundaries[0])
            {
                direction = true;
            }

            //Spawning Triggers
            weaponTriggerTimer-=Time.deltaTime;
            if (weaponTriggerTimer < 0)
            {
                weaponTriggerTimer = 2;
                if (weaponTriggers.Count < 3)
                {
                    GameObject _weaponTrigger = Instantiate(weaponTrigger, weaponTriggerParent, false);
                    _weaponTrigger.transform.localPosition = new Vector2(rnd.Next(boundaries[0], boundaries[1]), 0);
                    weaponTriggers.Add(_weaponTrigger);
                }
            }

            enemyTriggerTimer-=Time.deltaTime;
            if (enemyTriggerTimer < 0)
            {
                enemyTriggerTimer = 3;
                GameObject _enemyTrigger = Instantiate(enemyTrigger, weaponTriggerParent, false);
                _enemyTrigger.transform.localPosition = new Vector2(boundaries[1],0);
                enemyTriggers.Add(_enemyTrigger);
            }

            //Input
            if (Input.GetButtonDown("Fire1"))
            {
                BattleBarCursor _cursor = cursor.GetComponent<BattleBarCursor>();
                if (_cursor.isTouching)
                {
                    if (_cursor.triggerType == TriggerTypes.Weapon)
                    {
                        weaponTriggers.Remove(_cursor.collidingTrigger);
                        Destroy(_cursor.collidingTrigger);
                        player.attack(enemy);
                        _cursor.isTouching = false;
                    }
                    else if (_cursor.triggerType == TriggerTypes.Enemy)
                    {
                        enemyTriggers.Remove(_cursor.collidingTrigger);
                        Destroy(_cursor.collidingTrigger);
                        _cursor.isTouching = false;
                    }
                }
                else
                {
                    if (weaponTriggers.Count > 0)
                    {
                        int _index = rnd.Next(weaponTriggers.Count);
                        Destroy(weaponTriggers[_index]);
                        weaponTriggers.Remove(weaponTriggers[_index]);
                    }
                }
            }
            //Manage Enemy Triggers
            foreach (GameObject trigger in enemyTriggers.ToArray())
            {
                trigger.transform.localPosition = Vector2.Lerp(trigger.transform.localPosition, new Vector2(trigger.transform.localPosition.x - enemyTriggerSpeed, trigger.transform.localPosition.y), Time.deltaTime);
                if (trigger.transform.localPosition.x < boundaries[0])
                {
                    player.damage(rnd.Next(enemy.correspondingEnemy.damage[0],enemy.correspondingEnemy.damage[1]));
                    enemyTriggers.Remove(trigger);
                    Destroy(trigger);
                }
            }

            //check if enemy is dead
            if (enemy.correspondingEnemy.currentHealth <= 0)
            {
                gameManager.endBattle(true);
            }
            if (player.currentHealth <= 0)
            {
                gameManager.endBattle(false);
            }
        }
    }

    public void SetFighting()
    {
        isFighting = !isFighting;
    }

    public void ClearTriggers()
    {
        foreach (GameObject trigger in weaponTriggers.ToArray())
        {
            weaponTriggers.Remove(trigger);
            Destroy(trigger);
        }
        foreach (GameObject trigger in enemyTriggers.ToArray())
        {
            enemyTriggers.Remove(trigger);
            Destroy(trigger);
        }
    }
}
