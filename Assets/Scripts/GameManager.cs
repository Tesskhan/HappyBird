using System.Collections;
using System.Collections.Generic;
using TMPro; // Required for TextMeshPro
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class GameManager : MonoBehaviour
{
    // References to each game object's stop method
    public PipeGenerator pipeGeneratorScript;
    public Background backgroundScript;
    public Bird birdScript;

    // UI Elements
    public TextMeshProUGUI scoreText; // For displaying the current score
    public TextMeshProUGUI bestScoreText; // For displaying the best score
    public TextMeshProUGUI titleText; // Title text
    public TextMeshProUGUI startText; // Instructions text

    // Score Variables
    public int score = 0; // Current score
    private int bestScore = 0; // Best score
    private bool gameStarted = false; // Flag to track if the game has started
    public bool gameStopped = false; // Flag to track if the game is stopped

    // Audio
    public AudioSource gameOverAudioSource; // Reference to the AudioSource for the game over sound
    public AudioClip gameOverSound; // AudioClip for the game over sound

    // Start is called before the first frame update
    void Start()
    {
        // Find all relevant scripts on their respective objects
        pipeGeneratorScript = FindObjectOfType<PipeGenerator>();
        backgroundScript = FindObjectOfType<Background>();

        // Load best score from PlayerPrefs
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateBestScoreUI();

        // Show the title and instructions at the start
        ShowStartText();
        HideBestScore(); // Hide the best score at the start
    }

    // Update is called once per frame
    void Update()
    {
        // Start or restart the game when the player presses space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameStopped)
            {
                RestartGame(); // Reload the scene to restart
            }
            else if (!gameStarted)
            {
                StartGame(); // Start the game if it hasn't started yet
                
                // Make sure the bird is dynamic (affected by physics)
                birdScript.rb.simulated = true;

                // Start the pipe generation
                pipeGeneratorScript.StartGenerating();
            }
        }
    }

    // Method to start the game
    public void StartGame()
    {
        gameStarted = true;
        gameStopped = false;

        // Reset score
        score = 0;
        UpdateScoreUI();

        // Hide the title and instructions
        HideStartText();
        HideBestScore(); // Ensure the best score remains hidden during gameplay

        // Enable pipe generation
        if (pipeGeneratorScript != null)
        {
            pipeGeneratorScript.enabled = true;
        }
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

        // Play the game over sound
        if (gameOverAudioSource != null && gameOverSound != null)
        {
            gameOverAudioSource.PlayOneShot(gameOverSound); // Play the game over sound once
        }

        // Check and update best score
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore); // Save the best score
            PlayerPrefs.Save();
        }

        UpdateBestScoreUI();

        // Show everything again at the end of the game
        ShowStartText();
        ShowBestScore();

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

    // Method to update the current score
    public void UpdateScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    // Update the score UI
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // Update the best score UI
    private void UpdateBestScoreUI()
    {
        if (bestScoreText != null)
        {
            bestScoreText.text = "Best Score: " + bestScore;
        }
    }

    // Show the start text (title and instructions)
    private void ShowStartText()
    {
        if (titleText != null)
        {
            titleText.gameObject.SetActive(true);
        }

        if (startText != null)
        {
            startText.gameObject.SetActive(true);
        }
    }

    // Hide the start text (title and instructions)
    private void HideStartText()
    {
        if (titleText != null)
        {
            titleText.gameObject.SetActive(false);
        }

        if (startText != null)
        {
            startText.gameObject.SetActive(false);
        }
    }

    // Show the best score
    private void ShowBestScore()
    {
        if (bestScoreText != null)
        {
            bestScoreText.gameObject.SetActive(true);
        }
    }

    // Hide the best score
    private void HideBestScore()
    {
        if (bestScoreText != null)
        {
            bestScoreText.gameObject.SetActive(false);
        }
    }

    // Method to restart the game by reloading the scene
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
