using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] InputActionReference pause;

    public GameObject gameStartScreen;
    public GameObject gamePausedScreen;
    public GameObject quitGameScreen;

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
        PauseGame();
    }
}
