using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DealDamage : MonoBehaviour
{
    private Animator animator;
    private GameObject player = default;
    public int respawn = 0;

    private Text gameover;
    
    public GameObject ui;

    void Start()
    {
        gameover = ui.gameObject.transform.GetChild(10).GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            player = collider.gameObject;
            animator = player.GetComponent<Animator>();
            PlayerController pla = player.GetComponent<PlayerController>();
            pla.Damage();
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        if (animator.GetInteger("health") == 0) KeepData.lifes--;
        yield return new WaitForSeconds(1.3f);
        player.SetActive(false);
        if (KeepData.lifes <= 0)
        {
            Debug.Log("Game Over");
            gameover.text = "Game Over";
            gameover.enabled = true;
            yield return new WaitForSeconds(3f);
            if (KeepData.score > KeepData.highscore) KeepData.highscore = KeepData.score;
            KeepData.score = 0;
            KeepData.lifes = 3;
        }
        SceneManager.LoadScene(respawn);
    }
}
