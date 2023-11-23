using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    [Header("Basic Movement")]
    public float Speed;
    public float JumpForce; 
    private float Move;
    private float jump;
    [SerializeField]private Rigidbody2D RB;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask Groundlayer;

    [Header("Double Jump & Jump Buffer")]
    [SerializeField] private float DoubleJumpPower;
    private bool DoubleJump;
    private float JumpBufferTIme = 0.2f;
    private float JumpBufferCounter;
    [SerializeField]private float CoyoteTime = 0.1f;
    private float CoyoteTimeCounter;


    [Header("Wall Jump")]
    [SerializeField] private float WallSlideSpeed;
    private bool IsWallSlide;
    private bool IsWallJumping;
    private float WallJumpingDirection;
    private float WallJumpingTime = 0.2f;
    private float WallJumpingCounter;
    private float WallJumpingDuration = 0.4f;
    [SerializeField]private Vector2 WallJumpingPower = new Vector2 (10f, 20f);
    [SerializeField] private Transform WallCheck;
    [SerializeField] private LayerMask Walllayer;


    [Header("Alive")]
    [SerializeField] private Transform RespawnPoint;

    [Header("Animation")]
    [SerializeField] public Animator anim;

    [Header("Effect")]
    [SerializeField] private ParticleSystem dust;

    private bool Flipright = true;
    private bool isDead = false;

  
    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        if (DialogueManager.GetInstance().DialogueIsPlaying)
        {
            return;
        }

        if (GameManager.isPaused == false)
        {
            Move = Input.GetAxis("Horizontal");
            if (IsGrounded() && !Input.GetButton("Jump"))
            {
                anim.SetBool("IsJumping", false);
                DoubleJump = false;
            }

            if (IsGrounded())
            {
                CoyoteTimeCounter = CoyoteTime;
                anim.SetBool("IsJumping", false);
            }
            else
            {
                CoyoteTimeCounter -= Time.deltaTime;
            }

            if (Input.GetButtonDown("Jump"))
            {
                JumpBufferCounter = JumpBufferTIme;
                anim.SetBool("IsJumping", true);
                CreateDust();
            }
            else
            {
                JumpBufferCounter -= Time.deltaTime;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (CoyoteTimeCounter > 0f || DoubleJump && JumpBufferCounter > 0f)
                {
                    RB.velocity = new Vector2(RB.velocity.x, DoubleJump ? DoubleJumpPower : JumpForce);
                    DoubleJump = !DoubleJump;
                    JumpBufferCounter = 0f;
                    anim.SetBool("IsJumping", true);
                    CreateDust();
                }
            }

            if (Input.GetButtonUp("Jump") && RB.velocity.y > 0f)
            {
                anim.SetBool("IsJumping", true);
                RB.velocity = new Vector2(RB.velocity.x, RB.velocity.y * 0.5f);
                CoyoteTimeCounter = 0f;
                CreateDust();
            }

            anim.SetFloat("Speed", Mathf.Abs(Move));

            WallSlide();
            WallJump();
            Movement();
        }
           
            
       
        

    }

  
    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        Flipright = !Flipright;
        
    }
   
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, Groundlayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(WallCheck.position, 0.2f, Walllayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && Move != 0f)
        {
            IsWallSlide = true;
            RB.velocity = new Vector2(RB.velocity.x, Mathf.Clamp(RB.velocity.y, -WallSlideSpeed, float.MaxValue));
            CreateDust();
        }
        else
        {
            IsWallSlide = false;
        }
    }

    private void Movement()
    {
        if (Move != 0)
        {
            if (!IsWallJumping)
            {
                RB.velocity = new Vector2(Move * Speed, RB.velocity.y);
                CreateDust();
            }
        }

        if (Move > 0 && !Flipright)
        {
            if (!IsWallJumping)
            {
                Flip();
            }
        }

        if (Move < 0 && Flipright)
        {
            if (!IsWallJumping)
            {
                Flip();
            }
        }
    }

    private void WallJump()
    {
        if (IsWallSlide)
        {
            IsWallJumping = false;
            WallJumpingDirection = -transform.localScale.x;
            WallJumpingCounter = WallJumpingTime;
            
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            WallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && WallJumpingCounter > 0f)
        {
            IsWallJumping = true;
            RB.velocity = new Vector2(WallJumpingDirection * WallJumpingPower.x, WallJumpingPower.y);
            WallJumpingCounter = 0f;

            if(transform.localScale.x != WallJumpingDirection)
            {
                Flipright = !Flipright;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
                
            }

            Invoke(nameof(StopWallJumping), WallJumpingDuration);
        }

        
    }

    private void StopWallJumping()
    {
        IsWallJumping = false;
    }

    private void CreateDust()
    {
        if (IsGrounded())
        {
            if (dust == null)
                return;

            ParticleSystem.ShapeModule shapeParticell = dust.shape;
            shapeParticell.position = new Vector3(0f, 0f, 0f);
            shapeParticell.scale = new Vector3(1.35f, 0f, 1);
            dust.Play();
        }

        if (IsWallSlide)
        {
            if (dust == null)
                return;

            ParticleSystem.ShapeModule shapeParticell = dust.shape;
            shapeParticell.scale = new Vector3(0f, 2f, 1);
            if (Move > 0)
            {
                shapeParticell.position = new Vector3(0.55f, 1.3f, 0f);
                dust.Play();
            }
            else if(Move < 0)
            {
                shapeParticell.position = new Vector3(-0.55f, 1.3f, 0f);
                dust.Play();
            }
        }

    
    }

    private void Die()
    {
        isDead = true;  
        anim.SetTrigger("Dead");    
        StartCoroutine(Respawn(2f));
        
    }

    IEnumerator Respawn(float duration)
    {
        Debug.Log("Respawning...");
        yield return new WaitForSeconds(duration);
        transform.position = RespawnPoint.position;
        isDead = false;
        anim.SetTrigger("Alive");
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            Die();
        }
    }

    

}
