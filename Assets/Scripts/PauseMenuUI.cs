using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button quitButton;

    public void Start()
    {
        resumeButton.onClick.AddListener(OnResume);
        homeButton.onClick.AddListener(OnHome);
        quitButton.onClick.AddListener(OnQuit);
    }

    private void OnResume()
    {
        GameManager.Instance.ResumeGame();
        UIManager.Instance.ShowGamePlay();
    }

    private void OnHome()
    {
        SceneManager.LoadScene(0);
    }

    private void OnQuit()
    {
        // if playing in the editor, stop play mode. If in a build, quit the application.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
