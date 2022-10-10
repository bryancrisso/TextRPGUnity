using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TopDown
{
    public class LootBox : MonoBehaviour
    {
        private System.Random rnd;
        public float spawnDist = 1f;

        [Serializable]
        public struct LootDrop
        {
            public ItemObject item;
            public int amount;
        }

        [SerializeField]
        private LootDrop[] loot;

        public Sprite closed;
        public Sprite open;

        private bool opened = false;

        private SpriteRenderer spriteRenderer;


        // Start is called before the first frame update
        void Start()
        {
            rnd = new System.Random();
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            GetComponent<Interactable>().action = new Interactable.InteractAction(Open);
            spriteRenderer.sprite = closed;
        }

        void Open()
        {
            if (!opened)
            {
                opened = true;
                GetComponent<BoxCollider2D>().enabled = false;
                spriteRenderer.sprite = open;
                foreach (LootDrop lootDrop in loot)
                {
                    GameObject drop = Instantiate(lootDrop.item.pickup, new Vector2(transform.position.x + (float)rnd.NextDouble() * spawnDist, transform.position.y + (float)rnd.NextDouble() * spawnDist), new Quaternion());
                    drop.GetComponent<Pickup>().amount = lootDrop.amount;
                }
            }
        }
    }
}