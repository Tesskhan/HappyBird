using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    private bool mapStopped = false; // Flag to check if the map has been stopped
    private float pushForce = 20f; // Force to push the bird to the right

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collision detected with: {collision.gameObject.name}");
        
        // Check if the object touching the ceiling is tagged as "Bird"
        if (collision.gameObject.CompareTag("Bird"))
        {
            Debug.Log("Bird touched the ceiling!");
            DisableFreezePositionX(collision.gameObject); // Disable X position freeze
            PushBirdToRight(collision.gameObject); // Push the bird to the right
            StopMap(); // Stop the map movement
        }
    }

    // Function to stop the map movement
    void StopMap()
    {
        if (!mapStopped) // Ensure this happens only once
        {
            mapStopped = true;

            // Find all scripts controlling movement in the scene
            Background[] backgroundScripts = FindObjectsOfType<Background>();

            // Disable their Update methods to stop movement
            foreach (Background script in backgroundScripts)
            {
                script.enabled = false;
            }

            Debug.Log("Map movement stopped!");
        }
    }

    // Function to disable FreezePositionX constraint
    void DisableFreezePositionX(GameObject bird)
    {
        Rigidbody2D birdRb = bird.GetComponent<Rigidbody2D>(); // Get the Rigidbody2D of the bird
        if (birdRb != null)
        {
            // Remove the FreezePositionX constraint while keeping the FreezeRotation constraint
            birdRb.constraints = 0;
            Debug.Log("FreezePositionX disabled!");
        }
        else
        {
            Debug.LogError("The bird does not have a Rigidbody2D component!");
        }
    }

    // Function to push the bird to the right
    void PushBirdToRight(GameObject bird)
    {
        Rigidbody2D birdRb = bird.GetComponent<Rigidbody2D>(); // Get the Rigidbody2D of the bird
        if (birdRb != null)
        {
            float smallPushForce = 2f; // Adjust this value for a smaller nudge
            birdRb.AddForce(Vector2.right * smallPushForce, ForceMode2D.Impulse); // Apply a small impulse force to the right
            Debug.Log("Bird nudged to the right!");
        }
        else
        {
            Debug.LogError("The bird does not have a Rigidbody2D component!");
        }
    }
}
