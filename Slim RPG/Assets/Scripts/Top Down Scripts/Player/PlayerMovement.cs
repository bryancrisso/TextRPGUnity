using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class PlayerMovement : MonoBehaviour
    {
        public float baseSpeed = 5f;
        private float moveSpeed;


        public Rigidbody2D rb;
        public Animator animator;

        Vector2 movement;

        Vector2 previousMovement;

        public PlayerWeapon weaponScript;

        private void Start()
        {
            moveSpeed = baseSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            // Input
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            if (movement.sqrMagnitude > 0.01 && !weaponScript.isAttacking)
            {
                previousMovement = movement;
                animator.SetFloat("PHorizontal", previousMovement.x);
                animator.SetFloat("PVertical", previousMovement.y);
            }
            if (weaponScript.isAttacking)
            {
                moveSpeed = baseSpeed * 0.25f;
            }
            else
            {
                moveSpeed = baseSpeed;
            }
        }

        void FixedUpdate()
        {
            // Movement
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
}