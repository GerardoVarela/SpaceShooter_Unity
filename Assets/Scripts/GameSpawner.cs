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
        // StartCoroutine(LineSpawning());

        InvokeRepeating("SpawnEnemy", 0.5f, 3f);
        InvokeRepeating("SpawnDebris", 0.5f, 2f);
    }

    // IEnumerator LineSpawning()
    // {
    //     for (int i = 0; i < 5; i++)
    //     {
    //         SpawnEnemy();
    //         yield return new WaitForSeconds(0.5f);
    //     }
    // }

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
