using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //private PlayerInput playerInput;
    private Rigidbody2D rb;
    private PlayerStats player;
    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public Vector2 lastMovedVector;
    Animator animator;
    SpriteRenderer characterSprite;

    public enum FacingVector
    {
        horizontal,
        vertical
    }
    public FacingVector facingVector;

    public enum Facing
    {
        left,
        right
    }
    public Facing facing;
    public Facing lastFacing;

    void Awake()
    {
        // playerInput = new PlayerInput();
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is NULL!");
        }
    }

    void Start()
    {
        lastMovedVector = new Vector2(1, 0f);
        facingVector = FacingVector.horizontal;
        facing = Facing.right;
        lastFacing = facing;
        animator = GetComponentInChildren<Animator>();
        characterSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }
        moveDirection = InputManager.GetMoveDirection();
        rb.velocity = moveDirection * player.CurrentMoveSpeed;

        if (moveDirection.x != 0)
        {
            lastMovedVector = new Vector2(moveDirection.x, 0f);
            facingVector = FacingVector.horizontal;
            if (moveDirection.x > 0)
            {
                lastFacing = facing;
                facing = Facing.right;
            }
            else if (moveDirection.x < 0)
            {
                lastFacing = facing;
                facing = Facing.left;
            }
            else
            {
                facing = lastFacing;
            }
        }
        if (moveDirection.y != 0)
        {
            lastMovedVector = new Vector2(0f, moveDirection.y);
            facingVector = FacingVector.vertical;
        }
        if (moveDirection.x != 0 && moveDirection.y != 0)
        {
            lastMovedVector = moveDirection;
            facingVector = FacingVector.horizontal;
        }

        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            animator.SetFloat("speed", 1);
        }
        else
        {
            animator.SetFloat("speed", 0);
        }

        if (lastFacing == Facing.left)
        {
            characterSprite.flipX = true;
        }
        else
        {
            characterSprite.flipX = false;
        }
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

}
