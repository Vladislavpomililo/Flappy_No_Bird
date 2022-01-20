using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    [SerializeField] private InputField inputName;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject baseMenu;
    [SerializeField] private GameObject settingsMenu;

    void Start()
    {
        MainManager.Instance.LoadRecord();
        scoreText.text = MainManager.Instance.score.ToString();
        nameText.text = MainManager.Instance.name.ToString();
    }

    void Update()
    {
        MainManager.Instance.name = inputName.text;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        baseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        baseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }


    public void ExitApp()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

