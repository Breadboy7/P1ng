using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; // Assign different power-up prefabs in inspector
    public float spawnInterval = 10f;
    public float spawnAreaWidth = 8f;
    public float spawnAreaHeight = 4f;
    public int maxPowerUps = 2;

    private void Start()
    {
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (GameObject.FindGameObjectsWithTag("PowerUp").Length < maxPowerUps)
            {
                SpawnRandomPowerUp();
            }
        }
    }

    private void SpawnRandomPowerUp()
    {
        if (powerUpPrefabs.Length == 0) return;

        // Random position within play area
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2),
            Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2),
            0);

        // Random power-up type
        GameObject powerUpToSpawn = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];

        Instantiate(powerUpToSpawn, spawnPosition, Quaternion.identity);
    }
}
