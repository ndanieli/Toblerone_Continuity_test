using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour {

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;
    public CameraControl cameraControl;


    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;


    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded && cameraControl.zoomIn)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        if (cameraControl.zoomIn)
        {
            float h = Input.GetAxis("Horizontal");

            anim.SetFloat("Speed", Mathf.Abs(h));

            if (h * rb2d.velocity.x < maxSpeed)
                rb2d.AddForce(Vector2.right * h * moveForce);

            if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
                rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

            if (h > 0 && !facingRight)
                flip();
            else if (h < 0 && facingRight)
                flip();

            if (jump)
            {
                anim.SetTrigger("Jump");
                rb2d.AddForce(new Vector2(0f, jumpForce));
                jump = false;
            }
        }
    }

    private void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void FreezeHero()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void UnFreezeHero(Vector2 linearVelocity, float angularVelocity)
    {
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.velocity = linearVelocity;
        rb2d.angularVelocity = angularVelocity;
    }
}
