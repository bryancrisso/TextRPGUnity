using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TopDown
{
    public class Pickup : MonoBehaviour
    {
        public ItemObject item;
        public int amount;
        private bool active = true;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player" && active)
            {
                active = false;
                collision.GetComponent<PickupsManager>().Pickup(this);
                Destroy(gameObject);
            }
        }
    }
}
