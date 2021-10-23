using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyScript : MonoBehaviour
{
    public Enemy correspondingEnemy;
    public Canvas canvas;
    public Slider healthSlider;
    public TextMeshProUGUI nameText;

    public void Damage(int amount)
    {
        correspondingEnemy.currentHealth -= amount;
    }

    public void Initialise(Enemy enemy)
    {
        correspondingEnemy = enemy;
        TextureManager _tManager = GameObject.FindWithTag("TextureManager").GetComponent<TextureManager>();
        if (correspondingEnemy.id+1 <= _tManager.enemyTextures.Length)
        {
            transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = _tManager.enemyTextures[correspondingEnemy.id];
        }
        else
        {
            transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = _tManager.nullTexture;
        }
    }

    private void Start()
    {
        canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        healthSlider.maxValue = correspondingEnemy.maxHealth;
        nameText.text = correspondingEnemy.name;
    }

    private void Update()
    {
        healthSlider.value = correspondingEnemy.currentHealth;
    }
}
