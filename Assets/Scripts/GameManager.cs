using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameSpawner gameSpawner;
    private MenuUIHandler menuUIHandler;

    public bool isGameActive = false;
    private int lives = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameSpawner = GameObject.Find("GameSpawner").GetComponent<GameSpawner>();
        menuUIHandler = GameObject.Find("Canvas").GetComponent<MenuUIHandler>();
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

    public int UpdateLives()
    {
        lives -= 1;
        menuUIHandler.UpdateLives(lives);

        if(lives == 0) 
            menuUIHandler.GameOver();

        return lives;
    }
}
