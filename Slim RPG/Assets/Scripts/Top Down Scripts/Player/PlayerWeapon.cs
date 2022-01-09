using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TopDown
{
    public class PlayerWeapon : MonoBehaviour
    {
        public WeaponScript equippedWeapon;

        private float currentAttackDelay;

        public Animator animator;

        public bool isAttacking;

        private int attackComboIndex = 0;
        private float currentComboDelay;

        // Start is called before the first frame update
        void Start()
        {
            currentAttackDelay = 0;
            currentComboDelay = equippedWeapon.comboDelay + equippedWeapon.weaponData.attackCombos[attackComboIndex].attackInterval;
            equippedWeapon.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (currentAttackDelay > 0 && !isAttacking)
            {
                currentAttackDelay -= Time.deltaTime;
            }

            if (!isAttacking)
            {
                currentComboDelay -= Time.deltaTime;
            }

            if (Input.GetButton("Fire1"))
            {
                if (currentAttackDelay <= 0)
                {
                    if (currentComboDelay < 0) //if time taken between attacks is too long then reset combo
                    {
                        attackComboIndex = 0;
                    }

                    currentAttackDelay = equippedWeapon.weaponData.attackCombos[attackComboIndex].attackInterval;
                    currentComboDelay = equippedWeapon.comboDelay + equippedWeapon.weaponData.attackCombos[attackComboIndex].attackInterval; //add on attack interval

                    //do attack

                    StartCoroutine("Attack");
                }
            }

            if (!isAttacking)
            {
                //look at cursor
                Vector3 WorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector3 Difference = WorldPoint - transform.position;
                Difference.Normalize();

                float RotationZ = Mathf.Atan2(Difference.y, Difference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, RotationZ);
            }
        }

        IEnumerator Attack()
        {
            float attackProgress = 0f;

            bool direction = equippedWeapon.weaponData.attackCombos[attackComboIndex].direction;

            isAttacking = true;

            equippedWeapon.gameObject.SetActive(true);

            float initialAngle = transform.rotation.eulerAngles.z;

            animator.SetTrigger("Attack");

            Vector2 snap = snapDirection(transform.rotation.eulerAngles.z); //angle is offset by 90 for some reason

            animator.SetFloat("PHorizontal", snap.x);
            animator.SetFloat("PVertical", snap.y);

            while (attackProgress < 1)
            {
                if (direction)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, initialAngle - equippedWeapon.weaponData.attackCombos[attackComboIndex].swingAngle / 2 + Mathf.Lerp(0, equippedWeapon.weaponData.attackCombos[attackComboIndex].swingAngle, attackProgress));

                    attackProgress += Time.deltaTime * 1 / equippedWeapon.weaponData.attackCombos[attackComboIndex].swingTime;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, initialAngle + equippedWeapon.weaponData.attackCombos[attackComboIndex].swingAngle / 2 - Mathf.Lerp(0, equippedWeapon.weaponData.attackCombos[attackComboIndex].swingAngle, attackProgress));

                    attackProgress += Time.deltaTime * 1 / equippedWeapon.weaponData.attackCombos[attackComboIndex].swingTime;
                }

                yield return null;
            }

            if (attackComboIndex < equippedWeapon.weaponData.attackCombos.Length - 1)
            {
                attackComboIndex++;
            }
            else
            {
                attackComboIndex = 0;
            }

            isAttacking = false;

            equippedWeapon.gameObject.SetActive(false);

            yield return null;
        }

        public static Vector2 snapDirection(float zRotation) //angles are offset by 90 degs
        {
            Vector2 direction = new Vector2(0f, 0f);

            if (zRotation > 0 && zRotation < 45)
            {
                direction = new Vector2(1f, 0f);
            }
            else if (zRotation > 45 && zRotation < 135)
            {
                direction = new Vector2(0f, 1f);
            }
            else if (zRotation > 135 && zRotation < 225)
            {
                direction = new Vector2(-1f, 0f);
            }
            else if (zRotation > 225 && zRotation < 315)
            {
                direction = new Vector2(0f, -1f);
            }
            else if (zRotation > 315 && zRotation < 360)
            {
                direction = new Vector2(1f, 0f);
            }

            return direction;
        }


        public void CollisionDetected(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();

                enemy.Damage(equippedWeapon.weaponData.attackCombos[attackComboIndex].damage);
            }
        }
    }
}