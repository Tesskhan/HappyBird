using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // References to each game object's stop method
    public PipeGenerator pipeGeneratorScript;
    public Background backgroundScript;
    public int score;
    private bool gameStopped = false; // Flag to track if the game is stopped

    // Start is called before the first frame update
    void Start()
    {
        // Find all relevant scripts on their respective objects
        pipeGeneratorScript = FindObjectOfType<PipeGenerator>();
        backgroundScript = FindObjectOfType<Background>();
    }

    // Method to stop the game components
    public void StopGame()
    {
        if (gameStopped) return; // Prevent stopping the game multiple times

        gameStopped = true;

        // Stop all pipes in the scene
        StopAllPipes();

        // Call stop methods for other objects
        if (pipeGeneratorScript != null)
        {
            pipeGeneratorScript.StopPipeGenerator();
        }

        if (backgroundScript != null)
        {
            backgroundScript.StopBackground();
        }

        Debug.Log("Game stopped!");
    }

    // Function to stop all pipes in the scene
    private void StopAllPipes()
    {
        // Find all pipes in the scene
        Pipe[] allPipes = FindObjectsOfType<Pipe>();

        // Loop through each pipe and stop its movement
        foreach (Pipe pipe in allPipes)
        {
            pipe.StopPipe(); // Stop each pipe
        }
    }

    // Method to update score
    public void UpdateScore(int points)
    {
        score += points;
       // if (scoreText != null)
        {
            // scoreText.text = "Score: " + score;
        }
    }
}
