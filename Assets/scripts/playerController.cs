using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    public Animator anim;
    public Joystick joystick;
    public string nomeDaSena;


    //estados de animação
    private enum State { idle, running, jumping, falling, hurt, crouch}
    private State state = State.idle;


    //variaveis modificaveis
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int cherries = 0;
    [SerializeField] private TextMeshProUGUI cherryText;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private int Health = 2;
    [SerializeField] private TextMeshProUGUI healthAmount;
    [SerializeField] private int diamonds = 0;
    [SerializeField] private TextMeshProUGUI DiamondsText;
    [SerializeField] private AudioSource Cherry;
    [SerializeField] private AudioSource Diamond;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        healthAmount.text = Health.ToString();
        Cherry = GetComponent<AudioSource>();
        Diamond = GetComponent<AudioSource>();

    }
    private void Update()
    {
        if (state != State.hurt)
        {
            Moviment();
        }
        AnimationState();
        anim.SetInteger("state", (int)state);  // define a animação com base no estado de enumeração

        if (Input.GetKeyDown(KeyCode.M))
        {
            Application.LoadLevel("Menu");
        }
        if (cherries == 16f)
        {
            Application.LoadLevel("Fase2");
            cherries -= 16;
        }
        if(diamonds == 7f)
        {
            Application.LoadLevel("Final");
            diamonds -= 7;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "coletavel")
        {
            Destroy(collision.gameObject);
            cherries += 1;
            Cherry.Play();
            cherryText.text = cherries.ToString();
        }
        if (collision.tag == "diamond")
        {
            Destroy(collision.gameObject);
            diamonds += 1;
            Diamond.Play();
            DiamondsText.text = diamonds.ToString();
        }
        if (collision.tag == "colisor")
        {
            state = State.hurt;
            Health -= 1;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.tag == "enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if (state == State.falling)
            {
                enemy.JumpedOn();
                Jump();
            }

            else
            {
                state = State.hurt;
                Health -= 1;
                healthAmount.text = Health.ToString();
                if (Health <= 0)
                {
                    Application.LoadLevel("Menu");
                }
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
    }

    private void Moviment()
    {
        float hDirection = joystick.Horizontal;


        // andar para esquerda
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            
        }
        //anadar para direita
        else if (hDirection > 0)
        {

            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
           
        }

        float verticalMove = joystick.Vertical;

        //pulo
        if (verticalMove >= 0.5f && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }

    void AnimationState()
    {

        if (state == State.jumping)
        {
            if (rb.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    
}
