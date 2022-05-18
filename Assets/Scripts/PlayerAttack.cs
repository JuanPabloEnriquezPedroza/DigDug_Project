using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private GameObject attackAreaUp = default;
    private GameObject attackAreaRight = default;
    private GameObject attackAreaLeft = default;
    private GameObject attackAreaDown = default;
    private bool attacking = false;
    private float timeToAttack = .5f;
    private float timer = 0f;

    void Start()
    {
        attackAreaUp = transform.GetChild(0).gameObject;
        attackAreaRight = transform.GetChild(1).gameObject;
        attackAreaLeft = transform.GetChild(2).gameObject;
        attackAreaDown = transform.GetChild(3).gameObject;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(animator.GetInteger("health") > 0)
        {
            if (Keyboard.current.spaceKey.isPressed)
            {
                Attack();
            }

            if (attacking)
            {
                timer += Time.deltaTime;

                if (timer >= timeToAttack)
                {
                    timer = 0;
                    attacking = false;
                    attackAreaDown.SetActive(attacking);
                    attackAreaRight.SetActive(attacking);
                    attackAreaUp.SetActive(attacking);
                    attackAreaLeft.SetActive(attacking);
                    animator.SetBool("attacking", attacking);
                }
            }
        }
    }

    private void Attack()
    {
        attacking = true;
        if (animator.GetInteger("direction") == 0) attackAreaDown.SetActive(attacking);
        if (animator.GetInteger("direction") == 1) attackAreaRight.SetActive(attacking);
        if (animator.GetInteger("direction") == 2) attackAreaUp.SetActive(attacking);
        if (animator.GetInteger("direction") == 3) attackAreaLeft.SetActive(attacking);
        animator.SetBool("attacking", attacking);
    }
}
