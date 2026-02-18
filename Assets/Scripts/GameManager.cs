using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Timer")]
    [SerializeField] private float gameDuration = 180f; // 3 minutes
    private float remainingTime;

    [Header("State")]
    public bool isGameRunning { get; private set; }
    public bool isPaused { get; private set; }

    [Header("Wave")]
    public int currentWave { get; private set; } = 1;

    public GamePlayUI gamePlayUI;

    void Awake()
    {
        Instance = this;
        if(gamePlayUI == null)
            gamePlayUI = FindObjectOfType<GamePlayUI>();
        Time.timeScale = 0; // Start paused until StartGame is called
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!isGameRunning || isPaused) return;

        UpdateTimer();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        remainingTime = gameDuration;
        isGameRunning = true;
        isPaused = false;

        gamePlayUI.UpdateWave(currentWave);
        gamePlayUI.UpdateTimer(remainingTime);
    }

    void UpdateTimer()
    {
        remainingTime -= Time.deltaTime;
        remainingTime = Mathf.Max(remainingTime, 0);

        gamePlayUI.UpdateTimer(remainingTime);

        if (remainingTime <= 0)
            EndGame();
    }

    public void NextWave(int wave)
    {
        currentWave = wave;
        gamePlayUI.UpdateWave(currentWave);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        gamePlayUI.SetPauseState(isPaused);
    }

    public void EndGame()
    {
        isGameRunning = false;
        Time.timeScale = 0;
        UIManager.Instance.ShowGameOverScreen();
    }
}
