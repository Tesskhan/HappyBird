using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        // Check if the object touching the ceiling is tagged as "Bird"
        if (collision.gameObject.CompareTag("Bird"))
        {
            Debug.Log("Bird touched the ceiling!");

            GameManager gameManager = FindObjectOfType<GameManager>();

            if (gameManager != null)
            {
                gameManager.StopGame();
            }
        }
    }
}
