using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float moveSpeed = 4f; // Speed of the background movement
    private float backgroundWidth = 20f; // Width of the background

    public GameObject secondBackground; // Reference to the second background object
    private bool isStopped = false; // Flag to stop the map movement

    // Update is called once per frame
    void Update()
    {
        if (isStopped)
        {
            return; // If the map is stopped, no further movement will occur
        }

        // Move both backgrounds to the left
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        secondBackground.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // Reset the first background when it moves out of view
        if (transform.position.x <= -backgroundWidth)
        {
            transform.position = new Vector3(secondBackground.transform.position.x + backgroundWidth, transform.position.y, transform.position.z);
        }

        // Reset the second background when it moves out of view
        if (secondBackground.transform.position.x <= -backgroundWidth)
        {
            secondBackground.transform.position = new Vector3(transform.position.x + backgroundWidth, secondBackground.transform.position.y, secondBackground.transform.position.z);
        }
    }

    // This method stops the map from moving
    public void StopBackground()
    {
        if (!isStopped) // Ensure this happens only once
        {
            isStopped = true; // Set the flag to true to stop movement
        }
    }
}
