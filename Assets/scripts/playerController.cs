using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{    
    private Rigidbody2D rb;
    private Collider2D coll;
    public Animator anim;


    //estados de animação
    private enum State { idle, running, jumping, falling }
    private State state = State.idle;


    //variaveis modificaveis
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int cherries = 0;
    [SerializeField] private Text cherryText;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    private void Update()
    {
        Moviment();

        AnimationState();
        anim.SetInteger("state", (int)state);  // define a animação com base no estado de enumaração
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "coletavel")
        {
            Destroy(collision.gameObject);
            cherries += 1;
            cherryText.text = cherries.ToString();
        }
    }

    private void Moviment()
    {
        float hDirection = Input.GetAxis("Horizontal");


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


        //pulo
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;
        }
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
