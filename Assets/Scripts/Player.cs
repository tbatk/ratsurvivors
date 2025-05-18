using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float speed = 3.0f;
    private Vector2 move = new Vector2(0, 0);

    private bool canDash = true;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer; 
    private bool isDashing;
    private float dashCooldown = 1f;
    private float dashDuration = 0.2f;
    private float dashSpeed = 20f;
    private int animationFrameRate = 12;
    private float idleTime;

    public List<Sprite> northSprites;
    public List<Sprite> southSprites;

    public List<Sprite> horizontalSprites;
    public List<Sprite> dashNorthSprites;
    public List<Sprite> dashSouthSprites;
    public List<Sprite> dashHorizontalSprites;

    void Start()
    {

    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    private void HandleFlip()
    {
        if (spriteRenderer.flipX && move.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (!spriteRenderer.flipX && move.x > 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void OnDash()
    {
        if (canDash && move != Vector2.zero)
        {
            StartCoroutine(Dash());
        }
    }

    private List<Sprite> GetDirectionalSprites()
    {
        List<Sprite> directionalSprites = null;
        if (!isDashing)
        {
            if (move.y > 0)
            {
                directionalSprites = northSprites;
            }
            else if (move.y < 0)
            {
                directionalSprites = southSprites;
            }
            else if (move.x != 0)
            {
                directionalSprites = horizontalSprites;
            }
            //TODO: Fix idle sprites
        }
        else
        {
            // Load dashing sprites
            if (move.y > 0)
            {
                directionalSprites = dashNorthSprites;
            }
            else if (move.y < 0)
            {
                directionalSprites = dashSouthSprites;
            }
            else if (move.x != 0)
            {
                directionalSprites = dashHorizontalSprites;
            }
            //TODO: Fix idle sprites
        }
        return directionalSprites;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        HandleFlip();
        List<Sprite> sprites = GetDirectionalSprites();
        if (!isDashing)
        {
            rb.linearVelocity = move * speed;
            //Idea from: https://www.youtube.com/watch?v=NQN3rYGqqP8
            if (move != Vector2.zero)
            {
                float playTime = Time.time - idleTime;
                float totalFrames = playTime * animationFrameRate;
                int frame = (int)(totalFrames % sprites.Count);
                spriteRenderer.sprite = sprites[frame];
            }
            else
            {
                idleTime = Time.time;
                spriteRenderer.sprite = sprites[0];
            }
        }
        else
        {
            if (move != Vector2.zero)
            {
                float playTime = Time.time - idleTime;
                float totalFrames = playTime * animationFrameRate;
                int frame = (int)(totalFrames % sprites.Count);
                spriteRenderer.sprite = sprites[frame];
            }
            else
            {
                idleTime = Time.time;
                spriteRenderer.sprite = sprites[0];
            }
        }
    }

    void Update()
    {
        // transform.position += new Vector3(move.x, move.y, 0) * speed * Time.deltaTime; 
    }

    
        private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        Vector2 dashDirection = move.normalized;
        rb.linearVelocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        // Reset velocity after dash ends (optional: zero or walk speed)
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
