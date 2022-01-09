using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBar
{

    public class BattleBarCursor : MonoBehaviour
    {
        public bool isTouching;
        public string collisionTag;
        public TriggerTypes triggerType;
        public GameObject collidingTrigger;

        private void OnTriggerEnter(Collider collision)
        {
            isTouching = true;
            if (collision.gameObject.tag == "WeaponTrigger")
            {
                collisionTag = collision.gameObject.tag;
                triggerType = TriggerTypes.Weapon;
            }
            else if (collision.gameObject.tag == "EnemyTrigger")
            {
                collisionTag = collision.gameObject.tag;
                triggerType = TriggerTypes.Enemy;
            }
            collidingTrigger = collision.gameObject;
        }
        private void OnTriggerExit(Collider collision)
        {
            isTouching = false;
        }

    }

}