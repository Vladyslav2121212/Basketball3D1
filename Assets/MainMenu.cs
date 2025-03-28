using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject shopUI;
    public InputField playerNameInput;
    public Text playerNameText;
    public Text highScoreText;

    private string playerName;
    private int highScore;
    private bool isPaused = true;

    void Start()
    {
        Time.timeScale = 0f;
        menuUI.SetActive(true);
        shopUI.SetActive(false);

        playerName = PlayerPrefs.GetString("PlayerName", "Гравець");
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        playerNameText.text = "Гравець: " + playerName;
        highScoreText.text = "Рекорд: " + highScore;

        StartCoroutine(WaitForScoreManager());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (shopUI.activeSelf)
            {
                CloseShop();
            }
            else
            {
                TogglePauseMenu();
            }
        }
    }

    IEnumerator WaitForScoreManager()
    {
        yield return new WaitUntil(() => ScoreManager.Instance != null);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "Рекорд: " + highScore;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        menuUI.SetActive(false);
        isPaused = false;

        if (!string.IsNullOrEmpty(playerNameInput.text))
        {
            playerName = playerNameInput.text;
            PlayerPrefs.SetString("PlayerName", playerName);
            PlayerPrefs.Save();
        }
    }

    public void OpenShop()
    {
        menuUI.SetActive(false);
        shopUI.SetActive(true);
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        menuUI.SetActive(true);
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        menuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ExitGame()
    {
        if (ScoreManager.Instance != null)
        {
            int currentScore = ScoreManager.Instance.GetScore();
            if (currentScore > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", currentScore);
                PlayerPrefs.Save();
                Debug.Log("Новый рекорд сохранён: " + currentScore);
            }
        }

        Debug.Log("Выход из игры");
        Application.Quit();
    }
}






