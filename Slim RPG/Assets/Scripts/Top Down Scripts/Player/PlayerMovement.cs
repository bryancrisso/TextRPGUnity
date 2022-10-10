using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

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

        public TextMeshProUGUI movetext;

        public PlayerWeapon weaponScript;

        private void Start()
        {
            moveSpeed = baseSpeed;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            movement = context.ReadValue<Vector2>();
        }

        // Update is called once per frame
        void Update()
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", Vector2.ClampMagnitude(movement, 1).magnitude);

            if (movement.sqrMagnitude > 0 && !weaponScript.isAttacking)
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
            movetext.text = movement.x +" "+ movement.y;
        }

        void FixedUpdate()
        {
            // Movement
            rb.MovePosition(rb.position + Vector2.ClampMagnitude(movement,1) * moveSpeed * Time.fixedDeltaTime);
        }
    }
}