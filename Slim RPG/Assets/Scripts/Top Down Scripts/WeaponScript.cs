using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public enum AttackType
    {
        Swing,
        Thrust
    }

    public class WeaponScript : MonoBehaviour
    {
        public WeaponAttack[] attackCombos;
        public float comboDelay; //the amount of time to wait before resetting a combo

        private void OnTriggerEnter2D(Collider2D collision)
        {
            transform.parent.GetComponent<PlayerWeapon>().CollisionDetected(collision);
        }
    }

    [System.Serializable]
    public class WeaponAttack
    {
        public int damage;
        public float attackInterval; //rest time after attack
        public float swingTime; //how long the attack lasts for
        public float swingAngle; //when swinging, how much angle does the swing have
        public AttackType attackType; //attack type, thrust or swing
        public bool direction; //true for left to right, false for right to left
    }
}