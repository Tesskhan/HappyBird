using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the background movement
    public float backgroundWidth = 20f; // Width of the background

    public GameObject secondBackground; // Reference to the second background object

    // Update is called once per frame
    void Update()
    {
        // Move both backgrounds to the left
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        secondBackground.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // Reset the first background when it moves out of view
        if (transform.position.x <= -backgroundWidth)
        {
            transform.position = new Vector3(secondBackground.transform.position.x + backgroundWidth + 5, transform.position.y, transform.position.z);
        }

        // Reset the second background when it moves out of view
        if (secondBackground.transform.position.x <= -backgroundWidth)
        {
            secondBackground.transform.position = new Vector3(transform.position.x + backgroundWidth + 5, secondBackground.transform.position.y, secondBackground.transform.position.z);
        }
    }
}
