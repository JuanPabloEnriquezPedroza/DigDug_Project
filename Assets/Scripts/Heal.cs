using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Pathfinding;

public class Heal : MonoBehaviour
{
    private Animator animator;
    private float timeToHeal = 1f;
    private float timer = 0f;
    private int damage;

    public int points = 1000;

    public AIPath aiPath;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() 
    {
        //Direction
        if (aiPath.desiredVelocity.x >= .001f) transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (aiPath.desiredVelocity.x <= -.001f) transform.localScale = new Vector3(1f, 1f, 1f);

        //Damage
        damage = animator.GetInteger("damage"); 
        if (damage > 0)
        {
            aiPath.canMove = false;

            timer += Time.deltaTime;

            if (timer >= timeToHeal)
            {
                timer = 0;
                damage--;
                animator.SetInteger("damage", damage);
            }
        }
        else
        {
            aiPath.canMove = true;
        }    
    }

    public void SetTimerZero()
    {
        timer = 0f;
        Debug.Log("Timer reset:" + timer);
    }
}
