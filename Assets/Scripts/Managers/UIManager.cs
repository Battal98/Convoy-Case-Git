using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("-- UI Menus --")]
    public GameObject Canvas;
    public GameObject MainMenu;
    public GameObject GameMenu;
    public GameObject WinMenu;
    public GameObject LoseMenu;

    public TextMeshProUGUI LevelsText;

    [Header("-- Buttons --")]
    public Button NextLevelButton;
    public Button RestartButton;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
