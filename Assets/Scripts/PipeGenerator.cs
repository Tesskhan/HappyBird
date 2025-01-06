using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGenerator : MonoBehaviour
{
    public GameObject pipePrefab; // Reference to the pipe prefab
    public float spawnInterval = 3f; // Time between pipe spawns
    public float pipeSpawnX = 10f; // X position to spawn new pipes
    public float minY = -2f; // Minimum Y position for pipe
    public float maxY = 3f; // Maximum Y position for pipe
    private bool isStopped = false; // To control pipe generation

    private void Start()
    {
        // Start generating pipes
        StartCoroutine(GeneratePipes());
    }

    IEnumerator GeneratePipes()
    {
        while (!isStopped)
        {
            SpawnPipe();
            yield return new WaitForSeconds(spawnInterval); // Wait before spawning the next pipe
        }
    }

    void SpawnPipe()
    {
        // Randomize the Y position of the pipe
        float randomY = Random.Range(minY, maxY);

        // Instantiate the pipe at the spawn position
        Vector3 spawnPosition = new Vector3(pipeSpawnX, randomY, 0f);
        GameObject pipe = Instantiate(pipePrefab, spawnPosition, Quaternion.identity);

        // Set the tag of the newly instantiated pipe to "Pipe"
        pipe.tag = "Pipe";

        Debug.Log("Pipe spawned!");
    }

    public void StopPipeGenerator()
    {
        isStopped = true;
        Debug.Log("Pipe generation stopped!");
    }
}
