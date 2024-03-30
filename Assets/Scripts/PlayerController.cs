using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private Animator anim;

    private float xInput;

    private int facingDirection = 1;
    private bool facingRight = true;

    


    [Header("Collision")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;
    private bool isCrouched;



    // The sprite's transform
    [SerializeField]
    private Transform gunTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float angle = Mathf.Atan2(mouseWorldPosition.y - gunTransform.position.y, mouseWorldPosition.x - gunTransform.position.x) * Mathf.Rad2Deg;



        if(!facingRight)
        {
            if (angle < 0) angle += 360;

            angle = Mathf.Clamp(angle, 90, 270);

            gunTransform.rotation = Quaternion.Euler(180, 0, -angle);
        }else if(facingRight)
        {
            angle = Mathf.Clamp(angle, -90, 90);

            gunTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        


        Movement();
        CheckInput();

        CollisionChecks();

        FlipController();
        AnimatorControllers();

    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundMask);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(Input.GetKey(KeyCode.S) && isGrounded)
        {
            isCrouched = true;
            xInput = 0;
        }
        else
        {
            isCrouched = false;
        }
        
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        if(isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

    }

    private void AnimatorControllers()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isCrouched", isCrouched);


    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        gunTransform.Rotate(180, 180, 0);
    }

    private void FlipController()
    {
        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x,transform.position.y - groundCheckDistance));
    }

}
