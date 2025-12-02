using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] Transform spawnLineTop;
    [SerializeField] Transform spawnLineBottom;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LineSpawning());
    }

    IEnumerator LineSpawning()
    {
        Vector3 lineTop = spawnLineTop.position;
        Vector3 lineBottom = spawnLineBottom.position;

        for (int i = 0; i < 5; i++)
        {
            float t = Random.Range(0f, 1f);
            Vector3 startPosition = Vector3.Lerp(lineTop, lineBottom, t);
            
            Instantiate(enemyPrefab, startPosition, Quaternion.identity);
            
            yield return new WaitForSeconds(0.5f);
        }
    }

    // void Start()
    // {
    //     StartCoroutine(LineSpawning());
    // }


    // IEnumerator LineSpawning()
    // {
    //     for (int i = 0; i < 5; i++)
    //     {
    //         SpawnEnemy();
    //         yield return new WaitForSeconds(0.5f);
    //     }
    // }


    // float verticalBoundary = 0.75f;
    // float xSpawnPos = 2f;
    // void SpawnEnemy()
    // {
    //     float ySpawnPos = Random.Range(-verticalBoundary, verticalBoundary);
    //     Vector3 startPosition = new Vector3(xSpawnPos, ySpawnPos, 0);

    //     Instantiate(enemyPrefab, startPosition, enemyPrefab.transform.rotation);
    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}
