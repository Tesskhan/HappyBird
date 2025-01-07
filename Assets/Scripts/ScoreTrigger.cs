using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public GameManager gameManager;

    // Audio
    public AudioSource scoreAudioSource; // Reference to the AudioSource for the score sound
    public AudioClip scoreSound;         // AudioClip for the score sound

    // Trigger the event when the bird passes through
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bird") && !gameManager.gameStopped)
        {
            // Add points when the bird passes through the gap
            gameManager.UpdateScore(1);

            // Play the score sound
            if (scoreAudioSource != null && scoreSound != null)
            {
                scoreAudioSource.PlayOneShot(scoreSound); // Play the score sound once
            }

            Debug.Log("Bird passed through the pipes! Score awarded.");
        }
    }
}
