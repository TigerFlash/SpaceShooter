using UnityEngine;
public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab; // префаб босса
    public Transform[] bossSpawnPoints; // массив точек спавна
    public float spawnInterval = 60f; // интервал между спавнами
    private float lastSpawnTime;

    void Update()
    {
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            SpawnBoss(Random.Range(0, bossSpawnPoints.Length));
            lastSpawnTime = Time.time;
        }
    }
    public void SpawnBoss(int spawnPointIndex)
    {
        if (spawnPointIndex >= 0 && spawnPointIndex < bossSpawnPoints.Length)
        {
            Transform spawnPoint = bossSpawnPoints[spawnPointIndex];
            Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
        }
        else
        {
            Debug.LogWarning("Некорректный индекс точки спавна");
        }
    }
}
