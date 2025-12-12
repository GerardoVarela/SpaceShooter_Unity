using System.Collections;
using UnityEngine;

public class GameSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject[] debrisPrefabs;

    [SerializeField] Transform spawnLineTop;
    [SerializeField] Transform spawnLineBottom;

    private Vector3 lineTop;
    private Vector3 lineBottom;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineTop = spawnLineTop.position;
        lineBottom = spawnLineBottom.position;
    }

    private float startDelay = 0.5f;
    private float enemyInterval = 3.0f;
    private float debrisInterval = 2.0f;
    private float decreaseIntervalFactor = 10.0f;
    public void StartInvoking(int level)
    {
        enemyInterval -= level/decreaseIntervalFactor;
        debrisInterval -= level/decreaseIntervalFactor;
        InvokeRepeating("SpawnEnemy", startDelay, enemyInterval);
        InvokeRepeating("SpawnDebris", startDelay, debrisInterval);
    }

    public void CancelSpawnInvoking()
    {
        CancelInvoke();
    }

    void SpawnEnemy()
    {
        SpawnGameObject(enemyPrefabs);
    }

    void SpawnDebris()
    {
        SpawnGameObject(debrisPrefabs);
    }

    void SpawnGameObject(GameObject[] prefabs)
    {
        float t = Random.Range(0f, 1f);
        Vector3 startPosition = Vector3.Lerp(lineTop, lineBottom, t);
        
        int randomPrefab = Random.Range(0, prefabs.Length);

        Instantiate(prefabs[randomPrefab], startPosition, prefabs[randomPrefab].transform.rotation);
    }
}
