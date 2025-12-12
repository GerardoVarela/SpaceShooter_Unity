using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameSpawner gameSpawner;

    public bool isGameActive = false;
    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameSpawner = GameObject.Find("GameSpawner").GetComponent<GameSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        isGameActive = true;
        gameSpawner.StartInvoking();
    }
}
