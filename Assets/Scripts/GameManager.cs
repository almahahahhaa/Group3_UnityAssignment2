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

    private GamePlayUI ui;

    void Awake()
    {
        Instance = this;
        ui = FindObjectOfType<GamePlayUI>();
    }

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (!isGameRunning || isPaused) return;

        UpdateTimer();
    }

    public void StartGame()
    {
        remainingTime = gameDuration;
        isGameRunning = true;
        isPaused = false;

        ui.UpdateWave(currentWave);
        ui.UpdateTimer(remainingTime);
    }

    void UpdateTimer()
    {
        remainingTime -= Time.deltaTime;
        remainingTime = Mathf.Max(remainingTime, 0);

        ui.UpdateTimer(remainingTime);

        if (remainingTime <= 0)
            EndGame();
    }

    public void NextWave(int wave)
    {
        currentWave = wave;
        ui.UpdateWave(currentWave);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        ui.SetPauseState(isPaused);
    }

    public void EndGame()
    {
        isGameRunning = false;
        Time.timeScale = 0;
        ui.ShowGameOver();
    }
}
