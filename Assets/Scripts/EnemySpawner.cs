using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [Header("Objects")]
    public GameObject[] objectsToSpawn;
    public BoxCollider[] spawnPoints;
    public TextMeshProUGUI countdown;

    [Header("Time Interval")]
    public float minSpawnInterval = 2;
    public float maxSpawnInterval = 2;
    public float decayInterval = 0;

    float actualMinSpawn, actualMaxSpawn;

    private float timer;

    [Header("Spawn Amount")]
    public int minSpawnAmount = 1;
    public int maxSpawnAmout = 2;
    public float timeBetweenSpawn = 0.2f;


    private GameObject player;
    void Update()
    {
        actualMinSpawn -= Time.deltaTime * decayInterval;
        actualMaxSpawn -= Time.deltaTime * decayInterval;
    }
    private void Start()
    {
        player = GameObject.Find("Player");

        StartCoroutine(SpawnCooldown());

    }
    public IEnumerator SpawnCooldown()
    {

        actualMaxSpawn = maxSpawnInterval;
        actualMinSpawn = minSpawnInterval;
        while (player != null)
        {
            int r = Random.Range(minSpawnAmount, maxSpawnAmout + 1);

            for (int i = 0; i < r; i++)
            {
                SpawnObject();
                yield return new WaitForSeconds(timeBetweenSpawn);
            }
            yield return new WaitForSeconds(Random.Range(actualMinSpawn, actualMaxSpawn));
        }
    }
    public void SpawnObject()
    {
        var currentSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];


        Vector3 pos = new Vector3();

        var area = currentSpawnPoint.bounds;
        pos.x = Random.Range(area.min.x, area.max.x);
        pos.z = Random.Range(area.min.z, area.max.z);

        Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Length)], pos, Quaternion.identity);

    }
}
