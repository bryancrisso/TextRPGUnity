using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Path = System.IO.Path;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public enum enemyType
{
    Enemy
}

public class Enemies : MonoBehaviour
{
    public List<Enemy> enemyList = new List<Enemy>();

    private void Start()
    {
        if (!Directory.GetFiles(".").Contains(".\\enemies.json"))
        {
            File.Create(".\\enemies.json").Dispose();
        }

        string json = System.IO.File.ReadAllText(".\\enemies.json");
        enemyList = JsonConvert.DeserializeObject<List<Enemy>>(json);
    }
}

public class Enemy
{
    public string name;

    enemyType type = enemyType.Enemy;

    public int maxHealth;
    public int currentHealth;
    public int[] damage = { 0, 0 };
    public int[] level = { 0, 0 };
    public int[] gold = { 0, 0 };
    public int id;

    public void init(Enemy correspondingEnemy)
    {
        name = correspondingEnemy.name;
        type = correspondingEnemy.type;
        maxHealth = correspondingEnemy.maxHealth;
        damage = correspondingEnemy.damage;
        level = correspondingEnemy.level;
        gold = correspondingEnemy.gold;
        id = correspondingEnemy.id;

        currentHealth = maxHealth;
    }
}
