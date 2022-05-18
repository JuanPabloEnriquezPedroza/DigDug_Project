using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AttackArea : MonoBehaviour
{
    private int damage = 0;
    private Animator animator;
    private GameObject enemy = default;
    private GameObject player;

    private Text gameover;
    private Text scoreT;
    public GameObject ui;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scoreT = ui.gameObject.transform.GetChild(7).GetComponent<Text>();
        gameover = ui.gameObject.transform.GetChild(10).GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            enemy = collider.transform.GetChild(0).gameObject;
            animator = enemy.GetComponent<Animator>();
            damage = animator.GetInteger("damage") + 1;
            animator.SetInteger("damage", damage);

            Heal h = enemy.GetComponent<Heal>();
            h.SetTimerZero();

            if (damage > 4)
            {
                collider.gameObject.SetActive(false);
                KeepData.score += h.points;
                scoreT.text = KeepData.score.ToString();

                PlayerController pla = player.GetComponent<PlayerController>();
                pla.KillEnemy();
            }
            //Debug.Log(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        
    }
}
