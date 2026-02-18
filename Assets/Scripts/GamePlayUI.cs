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

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

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

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void UpdatePowerup(float simple, float smash)
    {
        simplePowerupSlider.value = simple;
        smashPowerupSlider.value = smash;
    }
}
