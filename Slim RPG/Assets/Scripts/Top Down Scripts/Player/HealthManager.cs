using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopDown
{
    public class HealthManager : MonoBehaviour
    {
        public float maxHealth;
        public float currentHealth;
        public Slider healthSlider;

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
            healthSlider.maxValue = maxHealth;
        }

        public void updateHealth()
        {
            healthSlider.value = currentHealth;
        }

        public void Heal(float health)
        {
            currentHealth = Mathf.Min(currentHealth + health, maxHealth);
            updateHealth();
        }

        public void Damage(float damage, string attacker)
        {
            currentHealth -= damage;
            updateHealth();
            if (currentHealth <= 0)
            {
                Die(attacker);
            }
        }

        void Die(string attacker)
        {
            Debug.Log("You died to " + attacker);
            Destroy(gameObject);
        }
    }
}