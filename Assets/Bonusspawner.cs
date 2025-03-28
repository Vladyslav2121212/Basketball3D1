using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public GameObject bonusPrefab; 
    public Transform hoopPosition; 
    public float spawnRadius = 1.5f; 
    public float spawnInterval = 5f; 

    private void Start()
    {
        InvokeRepeating(nameof(SpawnBonus), 2f, spawnInterval);
    }

    void SpawnBonus()
    {
        if (bonusPrefab == null || hoopPosition == null) return;

        Vector3 spawnPos = hoopPosition.position + new Vector3(
            Random.Range(-spawnRadius, spawnRadius),
            1f, // Висота спавну
            Random.Range(-spawnRadius, spawnRadius)
        );

        Instantiate(bonusPrefab, spawnPos, Quaternion.identity);
    }
}

