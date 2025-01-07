using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private GameManager gameManager;
    public Rigidbody2D rb; // Rigidbody2D reference
    private float jumpForce = 8f; // Adjust this to control the jump height
    private float pushForce = 5f; // Force to push the bird to the right
    private Animator animator;
    private bool isStopped = false;
    private bool impulsed = false;

    // Rotation values for tilt effect
    private float maxTiltAngle = 30f; // Maximum tilt angle (positive for up, negative for down)
    private float tiltSmoothness = 5f; // Smoothness of the tilt effect

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>(); // Get the Animator component on this GameObject
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }

        // Find the GameManager (you can also set this from the inspector if desired)
        gameManager = FindObjectOfType<GameManager>();

        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the bird

        // Freeze the X position so the bird cannot move horizontally
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        // Disable gravity at the start of the game
        rb.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Start the game when spacebar is pressed and game is not stopped
        if (Input.GetKeyDown(KeyCode.Space) && !isStopped)
        {
            // Apply an upward force on the Y-axis
            rb.velocity = new Vector2(0, 0); // Ensure the X velocity remains 0, and only Y velocity is used
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Apply an upward force
        }

        // Apply the tilt effect based on the bird's vertical velocity (y-axis)
        ApplyTiltEffect();
    }

    // When the bird collides with something
    void OnCollisionEnter2D(Collision2D collision)
    {
        isStopped = true;

        // Disable freeze on X position and push the bird to the right
        DisableFreezePositionX(); // Disable X position freeze for the bird itself
        PushBirdToRight(); // Push the bird to the right

        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        // Stop the game when the bird collides with anything
        if (gameManager != null)
        {
            gameManager.StopGame();
        }
    }

    // Function to disable FreezePositionX constraint on the bird's Rigidbody2D
    public void DisableFreezePositionX()
    {
        // Remove the FreezePositionX constraint while keeping the FreezeRotation constraint on the bird's Rigidbody2D
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
    }

    // Function to push the bird to the right with a small force
    public void PushBirdToRight()
    {
        if(!impulsed)
        {
            // Reset the bird's velocity to prevent interference from other forces
            rb.velocity = new Vector2(0, rb.velocity.y); // Keep the Y velocity, set X to 0

            // Apply a one-time, small impulse force to the right
            rb.AddForce(Vector2.right * pushForce, ForceMode2D.Impulse);

            impulsed = true;
        }
    }

    // Function to apply tilt effect based on the bird's vertical velocity (going up or down)
    void ApplyTiltEffect()
    {
        // Calculate tilt based on vertical velocity (going up = positive, going down = negative)
        float tilt = Mathf.Lerp(0, maxTiltAngle, Mathf.Abs(rb.velocity.y) / jumpForce);
        
        // If the bird is going up, tilt upwards (positive), if down, tilt downwards (negative)
        if (rb.velocity.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, tilt); // Tilt upwards
        }
        else if (rb.velocity.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -tilt); // Tilt downwards
        }

        // Smooth the tilt effect
        float currentZRotation = transform.rotation.eulerAngles.z;
        float targetZRotation = (rb.velocity.y > 0) ? tilt : (rb.velocity.y < 0) ? -tilt : 0;

        // Smooth transition of rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetZRotation), Time.deltaTime * tiltSmoothness);
    }
}
