using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class Bobber : MonoBehaviour
    {
        private float startPos;
        public float magnitude = 1;
        public float speed = 1;

        // Start is called before the first frame update
        void Start()
        {
            startPos = this.transform.position.y;
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.position = new Vector2(transform.localPosition.x, startPos + magnitude*Mathf.Sin(speed * Time.fixedTime));
        }
    }
}
