using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text playerGoalsText;
    [SerializeField] private TMP_Text enemyGoalsText;
    [SerializeField] private TMP_Text gameTimerText;
    [SerializeField] private TMP_Text waveCounterText;

    [SerializeField] private Slider simplePowerupSlider;
    [SerializeField] private Slider smashPowerupSlider;

    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pausePanel;

    public void Start()
    {
        pauseButton.onClick.AddListener(() => GameManager.Instance.TogglePause());
    }

    public void UpdateWave(int wave)
    {
        waveCounterText.text = $"Wave {wave}";
    }

    public void UpdateTimer(float time)
    {
        int min = Mathf.FloorToInt(time / 60);
        int sec = Mathf.FloorToInt(time % 60);
        gameTimerText.text = $"{min:00}:{sec:00}";
    }

    public void SetPauseState(bool paused)
    {
        pausePanel.SetActive(paused);
    }

    public void UpdatePlayerGoals(int goals)
    {
        playerGoalsText.text = $"You: {goals}";
    }

    public void UpdateEnemyGoals(int goals)
    {
        enemyGoalsText.text = $"AI: {goals}";
    }

    public void ActivePowerup(PowerupType type)
    {
        switch (type)
        {
            case PowerupType.Simple:
                simplePowerupSlider.gameObject.SetActive(true);
                break;
            case PowerupType.Smash:
                smashPowerupSlider.gameObject.SetActive(true);
                break;
        }
    }
    public void UpdateSimplePowerup(float value)
    {
        simplePowerupSlider.value = value;
    }

    public void UpdateSmashPowerup(float value)
    {
        smashPowerupSlider.value = value;
    }

    public void HidePowerup()
    {
        simplePowerupSlider.gameObject.SetActive(false);
    }

    public void HideSmashPowerup()
    {
        smashPowerupSlider.gameObject.SetActive(false);
    }
}
