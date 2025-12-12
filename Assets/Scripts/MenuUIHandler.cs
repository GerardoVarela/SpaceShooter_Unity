using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;





#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [Header("Input Action")]

    [SerializeField] InputActionReference pause;

    [Header("Screens")]

    public GameObject gameStartScreen;
    public GameObject gameOnScreen;
    public GameObject gamePausedScreen;
    public GameObject gameOverScreen;
    public GameObject quitGameScreen;

    [Header("Lives")]
    public Image[] livesImages;
    public Sprite heartFull;
    public Sprite heartEmpty;

    [Header("Threats")]
    public TextMeshProUGUI threatDebrisText;
    public TextMeshProUGUI threatSinusoidalText;
    public TextMeshProUGUI threatZPatternText;

    [Header("Level")]
    public TextMeshProUGUI levelText;

    private GameManager gameManager;

    void OnEnable()
    {
        pause.action.Enable();
        pause.action.started += OnPause;
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnDisable()
    {
        pause.action.Disable();
        pause.action.started -= OnPause;
    }

    public void UpdateLives(int lives)
    {
        for (int i = 0; i < livesImages.Length; i++)
        {
            livesImages[i].sprite = (livesImages.Length - i <= lives) ? heartFull : heartEmpty;
        }
    }

    public void UpdateThreats(Dictionary<GameManager.ThreatTypes, ThreatConfig> threats)
    {
        ThreatConfig debrisConfig = threats[GameManager.ThreatTypes.Debris];
        ThreatConfig sinusoidalConfig = threats[GameManager.ThreatTypes.Sinusoidal];
        ThreatConfig zPatternConfig = threats[GameManager.ThreatTypes.ZPattern];
        
        threatDebrisText.text = $"{debrisConfig.qty}/{debrisConfig.max}";
        threatSinusoidalText.text = $"{sinusoidalConfig.qty}/{sinusoidalConfig.max}";
        threatZPatternText.text = $"{zPatternConfig.qty}/{zPatternConfig.max}";
    }

    public void UpdateLevel(int level)
    {
        levelText.text = "Level " + level;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gamePausedScreen.SetActive(true);
        quitGameScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        gamePausedScreen.SetActive(false);
        quitGameScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void StartGame() {
        gameManager.StartGame();
        gameStartScreen.SetActive(false);
        levelText.gameObject.SetActive(true);
        quitGameScreen.SetActive(false);
        gameOnScreen.SetActive(true);
    }

    public void GameOver() {
        quitGameScreen.SetActive(true);
        gameOverScreen.SetActive(true);
    }

    public void RestartGame() {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (gameManager.isGameActive) PauseGame();
    }
}
