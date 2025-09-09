using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [Header("Настройки спавна")]
    public GameObject[] spawnPrefabs;
    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 10f;
    private HashSet<GameObject> spawnedPrefabs = new HashSet<GameObject>();
    private HashSet<GameObject> aliveObjects = new HashSet<GameObject>();
    /*
     Доработать
     */
    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            TrySpawnUniqueObject();
        }
    }

    private void TrySpawnUniqueObject()
    {
        List<GameObject> availablePrefabs = new List<GameObject>();
        foreach (var prefab in spawnPrefabs)
        {
            if (!spawnedPrefabs.Contains(prefab))
            {
                availablePrefabs.Add(prefab);
            }
        }

        if (availablePrefabs.Count == 0)
        {
            return;
        }
        int index = Random.Range(0, availablePrefabs.Count);
        GameObject prefabToSpawn = availablePrefabs[index];
        GameObject spawnedObj = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        spawnedPrefabs.Add(prefabToSpawn);
        aliveObjects.Add(spawnedObj);
        DestroyEventTrigger destroyTrigger = spawnedObj.AddComponent<DestroyEventTrigger>();
        if (destroyTrigger != null)
        {
            destroyTrigger.OnDestroyed += () =>
            {
                aliveObjects.Remove(spawnedObj);
            };
        }
        else
        {

        }
    }
}