using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Screens")]
    [SerializeField] private GameObject homeMenuScreen;
    [SerializeField] private GameObject controlsScreen;
    [SerializeField] private GameObject gamePlayScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject gameOverScreen;
    private void Awake()
    {
       if(Instance==null)
            Instance = this;
    }

    public void ShowHomeMenu()
    {
        HideAllScreens();
        homeMenuScreen.SetActive(true);
    }

    public void ShowControls()
    {
        HideAllScreens();
        controlsScreen.SetActive(true);
    }


    public void ShowGamePlay()
    {
        HideAllScreens();
        gamePlayScreen.SetActive(true);
    }

    public void ShowPauseScreen()
    {
        HideAllScreens();
        pauseScreen.SetActive(true);
    }

    public void ShowGameOverScreen()
    {
        HideAllScreens();
        gameOverScreen.SetActive(true);
    }

    private void HideAllScreens()
    {
        homeMenuScreen.SetActive(false);
        controlsScreen.SetActive(false);
        gamePlayScreen.SetActive(false);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }
}
