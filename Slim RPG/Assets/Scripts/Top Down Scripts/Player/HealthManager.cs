using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class HealthManager : MonoBehaviour
    {
        public float maxHealth;
        public float currentHealth;

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
        }

        public void Damage(float damage, string attacker)
        {
            currentHealth -= damage;
            Debug.Log(currentHealth);
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