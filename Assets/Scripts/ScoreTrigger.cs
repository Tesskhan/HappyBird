using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public GameManager gameManager;

    // Trigger the event when the bird passes through
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bird"))
        {
            // Add points when the bird passes through the gap
            gameManager.UpdateScore(1);
            Debug.Log("Bird passed through the pipes! Score awarded.");
        }
    }
}
