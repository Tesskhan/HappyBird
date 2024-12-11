using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb; // Rigidbody2D reference
    private float jumpForce = 8f; // Adjust this to control the jump height

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the bird

        // Freeze the X position so the bird cannot move horizontally
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Apply an upward force on the Y-axis
            rb.velocity = new Vector2(0,0); // Ensure the X velocity remains 0, and only Y velocity is used
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Apply an upward force
        }
    }
}
