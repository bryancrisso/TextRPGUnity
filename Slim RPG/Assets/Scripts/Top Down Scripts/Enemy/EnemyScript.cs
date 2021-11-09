using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public enum EnemyType
    {
        Melee,
        Ranged,
        Dummy
    }

    public class EnemyScript : MonoBehaviour
    {
        public string enemyName;
        public int currentHealth;
        public int maxHealth;
        public EnemyType enemyType;

        public Animator animator;

        [Header("Attacking")]
        public int attackDamage;
        public float attackRange;
        public float attackInterval;
        private float currentAttackDelay = 0;
        private bool isAttacking;

        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
            animator = gameObject.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (currentAttackDelay > 0 && !isAttacking)
            {
                currentAttackDelay -= Time.deltaTime;
            }
        }

        public void Damage(int damage)
        {
            currentHealth -= damage;

            animator.SetTrigger("Damage");

            if (currentHealth <= 0 && enemyType != EnemyType.Dummy)
            {
                Die();
            }
        }

        public void Die() //dies
        {
            Destroy(gameObject);
        }

        public void Attack(Transform target) //performs a melee attack (instant)
        {
            if (currentAttackDelay <= 0)
            {
                currentAttackDelay = attackInterval;

                Collider2D[] hits = Physics2D.OverlapCircleAll(target.position, 0.25f); //idk why but like i felt i should just check if the player is at the target point and damage it, nice and simple

                foreach(Collider2D hit in hits) // loop through the objects that hit the overlap circle
                {
                    if (hit.tag == "Player")
                    {
                        hit.GetComponent<HealthManager>().Damage(attackDamage, enemyName);
                    }
                }
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
