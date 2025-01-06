using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    private float moveSpeed = 4f; // Speed at which the pipe moves
    private float destroyPositionX = -6f; // X position to destroy the pipe when off-screen
    private bool isStopped = false; // Flag to check if the pipe should stop moving

    void Start()
    {
        // Check if this pipe is the default pipe, and if so, prevent it from moving
        if (CompareTag("DefaultPipe"))
        {
            isStopped = true; // Stop this pipe from moving
        }
    }

    void Update()
    {
        // If the pipe is not stopped, continue moving
        if (!isStopped)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        // Destroy the pipe when it moves out of the screen
        if (transform.position.x <= destroyPositionX && !CompareTag("DefaultPipe"))
        {
            Destroy(gameObject); // Destroy all pipes except the default pipe
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the pipe collides with the "Bird" tagged object
        if (collision.gameObject.CompareTag("Bird"))
        {
            Debug.Log("Pipe touched the Bird!");

            // Optionally, stop the game if the pipe hits the bird
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.StopGame();
            }
        }
    }

    // Function to stop the pipe's movement
    public void StopPipe()
    {
        isStopped = true;
    }
}
