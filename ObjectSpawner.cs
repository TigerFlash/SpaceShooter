using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public List<SpawnableObject> spawnObjects; // ������ �������� � �������
    public Transform pointA;  // ������ ����� ���������
    public Transform pointB;  // ������ ����� ���������
    public float spawnInterval = 2f; // �������� ����� ��������
    public float spawnDecrement = 0.1f; // �� ������� ����������� ��������
    public float increaseInterval = 20f; // �������� ���������� ��������
    private float timer = 0f;

    private float minX, maxX, minY, maxY;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= increaseInterval)
        {
            if (spawnInterval > 0.5f)
            {
                spawnInterval -= spawnDecrement;
            }
            timer = 0f;
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            minX = Mathf.Min(pointA.position.x, pointB.position.x);
            maxX = Mathf.Max(pointA.position.x, pointB.position.x);
            minY = Mathf.Min(pointA.position.y, pointB.position.y);
            maxY = Mathf.Max(pointA.position.y, pointB.position.y);
            float randX = Random.Range(minX, maxX);
            float randY = Random.Range(minY, maxY);
            Vector3 spawnPosition = new Vector3(randX, randY, 0f);
            GameObject objectToSpawn = GetRandomObject();
            if (objectToSpawn != null)
            {
                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private GameObject GetRandomObject()
    {
        float totalChance = 0f;
        foreach (var item in spawnObjects)
        {
            totalChance += item.spawnChance;
        }
        float rand = Random.Range(0f, totalChance);
        float cumulative = 0f;
        foreach (var item in spawnObjects)
        {
            cumulative += item.spawnChance;
            if (rand <= cumulative)
            {
                return item.prefab;
            }
        }
        return null; 
    }
}
[System.Serializable]
public class SpawnableObject
{
    public GameObject prefab;   // ������ ��� ������
    [Range(0f, 1f)]
    public float spawnChance = 1f; // ���� ������ (�� 0 �� 1)
}