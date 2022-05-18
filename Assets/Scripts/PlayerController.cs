using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float movementX;
    private float movementY;
    public int health = 1;
    public float speed = 1;
    public int enemies = 8;
    public int respawn = 0;

    private Image life3;
    private Image life2;
    private Image life1;
    private Text gameover;
    private Text scoreT;
    private Text highscoreT;

    public GameObject ui;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        life3 = ui.gameObject.transform.GetChild(2).GetComponent<Image>();
        life2 = ui.gameObject.transform.GetChild(1).GetComponent<Image>();
        life1 = ui.gameObject.transform.GetChild(0).GetComponent<Image>();
        highscoreT = ui.gameObject.transform.GetChild(5).GetComponent<Text>();
        scoreT = ui.gameObject.transform.GetChild(7).GetComponent<Text>();
        gameover = ui.gameObject.transform.GetChild(10).GetComponent<Text>();
        gameover.enabled = false;
        scoreT.text = KeepData.score.ToString();
        highscoreT.text = KeepData.highscore.ToString();
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
        bool mov = true;
        
        if (movementX > 0) animator.SetInteger("direction", 1);
        if (movementX < 0) animator.SetInteger("direction", 3);
        if (movementY > 0) animator.SetInteger("direction", 2);
        if (movementY < 0) animator.SetInteger("direction", 0);

        if (!Keyboard.current.wKey.isPressed && !Keyboard.current.aKey.isPressed && !Keyboard.current.sKey.isPressed && !Keyboard.current.dKey.isPressed) mov = false;

        animator.SetBool("walking", mov);
    }

    void FixedUpdate()
    {
        scoreT.text = KeepData.score.ToString();

        //Lifes
        if (KeepData.lifes == 3)
        {
            life3.enabled = true;
            life2.enabled = true;
            life1.enabled = true;
        }
        if (KeepData.lifes <= 2) life3.enabled = false;
        if (KeepData.lifes <= 1) life2.enabled = false;
        if (KeepData.lifes <= 0) life1.enabled = false;

        //Movement
        Vector2 movement = new Vector2(movementX, movementY);
        if (animator.GetBool("attacking") || health <= 0) rb.velocity = movement * 0;
        else rb.velocity = movement * speed;
    }

    public void Damage()
    {
        health--;
        animator.SetInteger("health", health);
        //Debug.Log(health);
    }

    public void KillEnemy()
    {
        enemies--;
        if (enemies <= 0) StartCoroutine(Win());
        //Debug.Log(health);
    }

    IEnumerator Win()
    {
        Debug.Log("You Win");
        gameover.text = "You Win";
        gameover.enabled = true;
        yield return new WaitForSeconds(3f);
        if (KeepData.score > KeepData.highscore) KeepData.highscore = KeepData.score;
        KeepData.score = 0;
        KeepData.lifes = 3;
        SceneManager.LoadScene(respawn);
    }
}