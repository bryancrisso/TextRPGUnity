using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public WeaponScript equippedWeapon;

    private float currentAttackDelay;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentAttackDelay = equippedWeapon.attackInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAttackDelay > 0)
        {
            currentAttackDelay -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1"))
        {
            if (currentAttackDelay <= 0)
            {
                currentAttackDelay = equippedWeapon.attackInterval;
                //do attack
                animator.SetTrigger("Attack");
                Debug.Log("among");
            }
        }

        //look at cursor
        Vector3 WorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 Difference = WorldPoint - transform.position;
        Difference.Normalize();

        float RotationZ = Mathf.Atan2(Difference.y, Difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, RotationZ);
    }
}
