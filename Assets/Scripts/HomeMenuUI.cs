using UnityEngine;
using UnityEngine.UI;

public class HomeMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button quitButton;


    private void Start()
    {
        playButton.onClick.AddListener(OnPlayClicked);
        controlsButton.onClick.AddListener(OnControlsClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
    }

    private void OnPlayClicked()
    {
        GameManager.Instance.StartGame();
        UIManager.Instance.ShowGamePlay();
    }

    private void OnControlsClicked()
    {
        UIManager.Instance.ShowControls();
    }

    private void OnQuitClicked()
    {
        // if playing in the editor, stop play mode. If in a build, quit the application.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
#endif
    }

}
