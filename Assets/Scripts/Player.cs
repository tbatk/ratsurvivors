using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float speed = 3.0f;
    private Vector2 move = new Vector2(0, 0);

    private bool canDash = true;
    public Rigidbody2D rb;
    private bool isDashing;
    private float dashCooldown = 1f;
    private float dashDuration = 0.2f;
    private float dashSpeed = 20f;

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

    public void OnDash()
    {
        if (canDash && move != Vector2.zero)
        {
            StartCoroutine(Dash());
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.linearVelocity = move * speed;
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
