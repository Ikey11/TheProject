using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10;   // Movement Speed Multiplier

    private Rigidbody2D rb;
    private Vector2 inputVector = new Vector2();
    private bool facingRight = true;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // If the input is moving the player right and the player is facing left
        if (inputVector.x > 0 && !facingRight)
            Flip();
        // If the input is moving the player left and the player is facing right
        else if (inputVector.x < 0 && facingRight)
            Flip();
    }

    void FixedUpdate()
    {
        // Move player
        rb.velocity = inputVector * moveSpeed;
    }

    public void Flip()
    {
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
