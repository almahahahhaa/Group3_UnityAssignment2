using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button quitButton;

    void Start()
    {
        homeButton.onClick.AddListener(OnHome);
        quitButton.onClick.AddListener(OnQuit);
    }

    public void ShowGameOver(int playerGoals, int opponentGoals)
    {
        finalScoreText.text = $"You : {playerGoals} |  Ai : {opponentGoals}";
    }

    public void OnHome()
    {
        SceneManager.LoadScene(0);
    }

    void OnQuit()
    {
        // if playing in the editor, stop play mode. If in a build, quit the application.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
