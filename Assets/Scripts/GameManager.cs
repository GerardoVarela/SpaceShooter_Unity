using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatConfig
{
    public GameManager.ThreatTypes threatTypes;
    public int max;
    public int qty;
}

public class GameManager : MonoBehaviour
{
    private AudioSource cameraAudioSource;
    public AudioClip levelUpClip;
    private GameSpawner gameSpawner;
    private MenuUIHandler menuUIHandler;

    public bool isGameActive = false;
    private int lives = 3;
    private int level = 1;
    private int maxThreatIncrementByLevel = 1;

    public enum ThreatTypes { Debris, Sinusoidal, ZPattern }
    Dictionary<ThreatTypes, ThreatConfig> threats = new()
    {
        { ThreatTypes.Debris,     new ThreatConfig() { max = 3, qty = 0 } },
        { ThreatTypes.Sinusoidal, new ThreatConfig() { max = 2, qty = 0 } },
        { ThreatTypes.ZPattern,   new ThreatConfig() { max = 1, qty = 0 } }
    };
    private int threatsObjectivesCompleted = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        gameSpawner = GameObject.Find("GameSpawner").GetComponent<GameSpawner>();
        menuUIHandler = GameObject.Find("Canvas").GetComponent<MenuUIHandler>();
    }

    public void StartGame()
    {
        menuUIHandler.UpdateThreats(threats);
        menuUIHandler.UpdateLevel(level);
        isGameActive = true;
        gameSpawner.StartInvoking(level);
    }

    public int UpdateLives()
    {
        lives -= 1;
        menuUIHandler.UpdateLives(lives);

        if(lives == 0) 
            menuUIHandler.GameOver();

        return lives;
    }

    public void ThreatDestroyed(ThreatTypes threatType)
    { 
        if (threats[threatType].qty < threats[threatType].max)
        {
            threats[threatType].qty += 1;
            menuUIHandler.UpdateThreats(threats);

            if (threats[threatType].qty == threats[threatType].max)
                threatsObjectivesCompleted += 1;
                if (threatsObjectivesCompleted == 3)
                    UpdateLevel();
        }
    }

    public void UpdateLevel()
    {
        level += 1;
        foreach (var threatConfig in threats.Values)
        {
            threatConfig.max += maxThreatIncrementByLevel;
            threatConfig.qty = 0;
        }
        threatsObjectivesCompleted = 0;
        cameraAudioSource.PlayOneShot(levelUpClip, 1.0f);
        menuUIHandler.UpdateLevel(level);
        menuUIHandler.UpdateThreats(threats);
        gameSpawner.CancelSpawnInvoking();
        gameSpawner.StartInvoking(level);
    }
}