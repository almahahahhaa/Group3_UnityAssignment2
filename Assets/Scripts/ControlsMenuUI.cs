using UnityEngine;
using UnityEngine.UI;

public class ControlsMenuUI : MonoBehaviour
{
    [SerializeField] private Button backButton;

    void Start()
    {
        backButton.onClick.AddListener(OnBack);
    }

    public void OnBack()
    {
        UIManager.Instance.ShowHomeMenu();
    }
}
