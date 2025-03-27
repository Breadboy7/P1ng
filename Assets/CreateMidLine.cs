using UnityEngine;

public class CreateMidLine : MonoBehaviour
{
    public GameObject prefab;  // Assign the prefab in the Inspector
    public int count = 10;     // Number of prefabs to spawn
    public float spacing = 1.5f; // Distance between each prefab along the Y-axis

    void Start()
    {
        SpawnPrefabs();
    }

    void SpawnPrefabs()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = new Vector3(0, i * spacing, 0);
            Instantiate(prefab, spawnPosition, Quaternion.identity);
            Instantiate(prefab, -spawnPosition, Quaternion.identity);
        }
    }
}
