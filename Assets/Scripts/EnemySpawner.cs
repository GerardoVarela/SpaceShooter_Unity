using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] Transform spawnLineTop;
    [SerializeField] Transform spawnLineBottom;

    Vector3 lineTop;
    Vector3 lineBottom;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineTop = spawnLineTop.position;
        lineBottom = spawnLineBottom.position;
        // StartCoroutine(LineSpawning());

        InvokeRepeating("SpawnEnemy", 0.5f, 2f);
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
        float t = Random.Range(0f, 1f);
        Vector3 startPosition = Vector3.Lerp(lineTop, lineBottom, t);
        
        Instantiate(enemyPrefab, startPosition, enemyPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
