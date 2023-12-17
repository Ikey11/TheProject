using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public float moveSpeed = 10;   // Movement Speed Multiplier
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    private Rigidbody2D rb;
    private Vector2 inputVector = new Vector2();
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        // Move player
        bool success = MovePlayer(inputVector);

        // Determine which axis that player cannot move
        if (!success)
        {
            // Try Left / Right
            success = MovePlayer(new Vector2(inputVector.x, 0));

            if (!success)
            {
                success = MovePlayer(new Vector2(0, inputVector.y));
            }
        }
    }

    public bool MovePlayer(Vector2 direction)
    {
        // Check for potential collisions
        int count = rb.Cast(
            direction,      // Represent the direction from the body to look for
            movementFilter, // The settings that determine where ac ollision can occur on such layers
            castCollisions, // List of collisions to store the found collisions into after Cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionOffset // The distance to cast equal to next frame moved
        );
        // No collisions
        if (count == 0)
        {
            Vector2 moveVector = direction * moveSpeed * Time.fixedDeltaTime;

            // Move character
            rb.MovePosition(rb.position + moveVector);
            return true;
        }
        else
        {
            // Print collisions
            foreach (RaycastHit2D hit in castCollisions)
            {
                print(hit.ToString());
            }

            return false;
        }
    }
}
