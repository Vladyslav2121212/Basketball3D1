using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public Text scoreText;
    public Text highScoreText;
    public AudioClip scoreSound;

    private int score = 0;
    private int highScore = 0;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        score = PlayerPrefs.GetInt("CurrentScore", 0);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreText();
    }

    public void AddScore()
    {
        score++;
        UpdateScoreText();

        PlayerPrefs.SetInt("CurrentScore", score);
        PlayerPrefs.Save();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            Debug.Log("Новий рекорд: " + highScore);
        }

        if (scoreSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(scoreSound);
        }
    }

    public int GetScore()
    {
        return score;
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }
}







