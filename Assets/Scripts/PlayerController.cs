using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private Animator playerAnim;
    [SerializeField] private Animator gunAnim;

    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashDuration;

    private float xInput;
    private int facingDirection = 1;
    private bool facingRight = true;

    [Header("Collision")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;

    private bool isGrounded;
    private bool isCrouched;
    private bool isFiring;
    private bool isDashing;

    private float dashDirection;
    private float dashActivationTimer;
    private float dashCooldownTimer;

    [Header("Gun")]
    [SerializeField] private Transform gunTransform;
    [SerializeField] private Transform muzzleTransform;
    [SerializeField] private Transform bulletTargetTransform;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform ejectionPointTransform;
    
	[Header("Particle Effect Prefabs")]
	[SerializeField] private GameObject bulletImpactFX;
	[SerializeField] private GameObject objectImpactFX;
	[SerializeField] private GameObject footstepFX;
	[SerializeField] private GameObject landFX;
	[SerializeField] private GameObject jumpFX;
	[SerializeField] private GameObject gotHitFX;
    [SerializeField] private GameObject bulletCasingEjectionFX;

    [Header("Gun Sounds")]
    [SerializeField] private AudioSource singleGunShotSound;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashCooldownTimer = dashCooldown;
    }

    IEnumerator Shoot()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(muzzleTransform.position, bulletTargetTransform.position - muzzleTransform.position);
        
        singleGunShotSound.Play();
        if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject.layer != 3)
        {
            Debug.Log(raycastHit2D.collider.attachedRigidbody);
            lineRenderer.SetPosition(0, muzzleTransform.position);
            lineRenderer.SetPosition(1, raycastHit2D.point);
            if (bulletCasingEjectionFX) Instantiate(bulletCasingEjectionFX, ejectionPointTransform.position, ejectionPointTransform.rotation);
            // fixme, we can maybe orient the fx with the impact normal
            // Quaternion.fromVector2D(raycastHit2D.normal)); or something?
            if (bulletImpactFX) Instantiate(bulletImpactFX,raycastHit2D.point,Quaternion.identity);
            if(raycastHit2D.collider.gameObject.layer == 9)
            {
                Debug.Log("Object Hit");
                raycastHit2D.collider.gameObject.GetComponent<DestructableObject>().TakeDamage(3);
				if (objectImpactFX) Instantiate(objectImpactFX,raycastHit2D.point,Quaternion.identity);
            }
        }
        else
        {
            lineRenderer.SetPosition(0, muzzleTransform.position);
            lineRenderer.SetPosition(1, bulletTargetTransform.position);
            //Debug.DrawRay(muzzleTransform.position, bulletTargetTransform.position - muzzleTransform.position, Color.blue, .1f);
        }

        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.02f);

        lineRenderer.enabled = false;
        isFiring = false;
    }


    void Update()
    {
        //Debug.DrawRay(muzzleTransform.position, bulletTargetTransform.position - muzzleTransform.position, Color.green, .1f);
        if (Input.GetMouseButtonDown(0))
        {
            isFiring = true;
            StartCoroutine(Shoot());

        }
        
       
        Aim();

        Movement();
        CheckInput();

        CollisionChecks();

        FlipController();
        AnimatorControllers();
    }

    private void Aim()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = transform.position.z - Camera.main.transform.position.z;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float angle = Mathf.Atan2(mouseWorldPosition.y - gunTransform.position.y, mouseWorldPosition.x - gunTransform.position.x) * Mathf.Rad2Deg;

        if (!facingRight)
        {
            if (angle < 0) angle += 360;

            angle = Mathf.Clamp(angle, 90, 270);

            gunTransform.rotation = Quaternion.Euler(180, 0, -angle);
        }
        else if (facingRight)
        {
            angle = Mathf.Clamp(angle, -90, 90);

            gunTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
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

        if(dashDirection != 0)
        {
            dashActivationTimer += Time.deltaTime;
        }

        dashCooldownTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.D) && !isDashing && dashCooldownTimer >= dashCooldown)
        {
            if(dashDirection == 0)
            {
                dashDirection = 1f;
                dashActivationTimer = 0;
                return;
            }

            isDashing = true;
            dashCooldownTimer = 0;
        }

        if (Input.GetKeyDown(KeyCode.A) && !isDashing && dashCooldownTimer >= dashCooldown)
        {
            if (dashDirection == 0)
            {
                dashDirection = -1f;
                dashActivationTimer = 0;
                return;
            }

            isDashing = true;
            dashCooldownTimer = 0;
        }

        if (!isDashing && dashActivationTimer > 0.5f)
        {
            dashDirection = 0;
        }

        if(dashCooldownTimer > dashDuration)
        {
            isDashing = false;
        }
        
    }

    private void Movement()
    {
        Debug.Log(isDashing);
        if (isDashing)
        {
            rb.velocity = new Vector2(dashDirection * dashSpeed, 0);
            return;
        }
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        if(isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			// emit a puff of dust offset to foot position
			if (jumpFX) Instantiate(jumpFX,new Vector2(transform.position.x,transform.position.y-1.5f),Quaternion.identity);

        }
    }

    private void AnimatorControllers()
    {
        bool isMoving = rb.velocity.x != 0;

        playerAnim.SetFloat("yVelocity", rb.velocity.y);
        playerAnim.SetBool("isMoving", isMoving);
        playerAnim.SetBool("isGrounded", isGrounded);
        playerAnim.SetBool("isCrouched", isCrouched);
        playerAnim.SetBool("isDashing", isDashing);
        gunAnim.SetBool("isFiring", isFiring);
        gunAnim.SetBool("isCrouched", isCrouched);
        gunAnim.SetBool("isGrounded", isGrounded);
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
