using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    [Header("Config")]
    public float Speed;
    public float JumpForce;
    public float WallSlideSpeed;
    public float WallJumpHorizontalForce;
    public float WallJumpVerticalForce;
    public float GroundCheckRadius;
    public float WallCheckDistance;
    [Header("References")]
    public Transform WallChecker;
    public Transform GroundChecker;
    public LayerMask WallLayer;
    public LayerMask GroundLayer;
    private Rigidbody2D Rig;
    private Animator Ani;
    private float HorizontalAxis;

    [Header("Properties")]
    public bool Right;
    public bool JumpButton;
    public bool Grounded;
    public bool CanDoubleJump;
    public bool CanMove;
    public bool Jumping;
    public bool Falling;
    public bool OnWall;
    public bool SlidingOnWall;
    public bool WallJumping;
    #endregion
    private void Awake()
    {
        FlipPlayer("Right");
        Rig = GetComponent<Rigidbody2D>();
        Ani = GetComponent<Animator>();
    }

    private void Update()
    {
        HorizontalAxis = Input.GetAxis("Horizontal");
        JumpButton = Input.GetButtonDown("Jump");

    }

    private void FixedUpdate()
    {
        Grounded = Physics2D.OverlapCircle(GroundChecker.position, GroundCheckRadius, GroundLayer);
        OnWall = Physics2D.Raycast(WallChecker.position, transform.right, WallCheckDistance, WallLayer);
        if (Falling && Jumping && !WallJumping) Ani.SetInteger("State", 3);
        if (Grounded)
        {
            Jumping = false;
            CanDoubleJump = false;
            WallJumping = false;
        }

        if (CanMove)
        {
            Rig.velocity = new Vector2(HorizontalAxis * Speed, Rig.velocity.y);
            if (Rig.velocity.x != 0 && !Jumping)
            {

                Ani.SetInteger("State", 1);
            }
            else if (Rig.velocity.x == 0 && !Jumping) Ani.SetInteger("State", 0);
            if (Rig.velocity.x > 0) FlipPlayer("Right");
            else if (Rig.velocity.x < 0) FlipPlayer("Left");
            if (Rig.velocity.y < 0) Falling = true;
            else if (Rig.velocity.y >= 0) Falling = false;
            #region Jump and Double Jump

            if (JumpButton && Grounded)
            {
                Ani.SetInteger("State", 2);
                Jumping = true;
                Rig.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                StartCoroutine(DoubleJump());
            }
            else if (JumpButton && CanDoubleJump && !Grounded && Jumping)
            {
                Ani.SetTrigger("Double Jump");
                Rig.velocity = new Vector2(Rig.velocity.x, 0);
                Rig.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                CanDoubleJump = false;
            }
            #endregion
        }

        #region Wall Jump
        if (Falling && HorizontalAxis != 0 && OnWall && !Grounded)
        {
            SlidingOnWall = true;
            WallJumping = false;
            Rig.velocity = new Vector2(Rig.velocity.x, -WallSlideSpeed);
            Ani.SetInteger("State", 4);
            if (HorizontalAxis > 0) FlipPlayer("Right");
            else if (HorizontalAxis < 0) FlipPlayer("Left");
            CanDoubleJump = false;
            
        }
        else SlidingOnWall = false;
        if (JumpButton && SlidingOnWall)
        {
            Rig.velocity = Vector2.zero;
            if (Right)
            {
                Rig.AddForce(new Vector2(-WallJumpHorizontalForce, WallJumpVerticalForce), ForceMode2D.Impulse);
                FlipPlayer("Left");
            }
            else
            {
                Rig.AddForce(new Vector2(WallJumpHorizontalForce, WallJumpVerticalForce), ForceMode2D.Impulse);
                FlipPlayer("Right");
            }
            WallJumping = true;
            Ani.SetInteger("State", 2);
            StartCoroutine(RemoveMoveDelay(0.3f));
        }
        #endregion


    }
    IEnumerator DoubleJump()
    {
        yield return new WaitForSeconds(0.05f);
        CanDoubleJump = true;
    }

    IEnumerator RemoveMoveDelay(float Delay)
    {
        DisableMove();
        Ani.SetInteger("State", 2);
        HorizontalAxis = 0;
        yield return new WaitForSeconds(Delay);
        WallJumping = false;
        EnableMove();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(GroundChecker.position, GroundCheckRadius);
        if (Right)
            Gizmos.DrawLine(WallChecker.position, new Vector3(WallChecker.position.x + WallCheckDistance, WallChecker.position.y, WallChecker.position.z));
        else
            Gizmos.DrawLine(WallChecker.position, new Vector3(WallChecker.position.x - WallCheckDistance, WallChecker.position.y, WallChecker.position.z));
    }

    public void FlipPlayer(string side)
    {
        switch (side)
        {
            case "Right":
                Right = true;
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case "Left":
                Right = false;
                transform.eulerAngles = new Vector3(0, 180, 0);
                break;
        }
    }
    public void EnableMove()
    {
        CanMove = true;
    }

    public void DisableMove()
    {
        CanMove = false;
    }
}
