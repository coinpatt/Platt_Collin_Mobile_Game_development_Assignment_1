using UnityEngine;
using UnityEngine.UI;
using TMPro; // Include this namespace for TextMeshPro
using System.Collections; // Include this for IEnumerator

public class GameManager : MonoBehaviour
{
    public TMP_Text timerText; // Use TMP_Text for TMPro text components
    public TMP_Text highScoreText; // Use TMP_Text for TMPro text components
    public GameObject mainCharacter; // Reference to the main character GameObject
    public Button saveButton; // Public button to save the high score
    public Button restartButton; // Public button to restart the game

    private float gameTime = 0f;
    private float highScore = 0f;
    private bool gameRunning = true;

    private void Start()
    {
        LoadHighScore();
        UpdateHighScoreText();
        StartCoroutine(UpdateTimeCoroutine());
    }

    private void Update()
    {
        if (!gameRunning)
            return;

        gameTime += Time.deltaTime;
        UpdateTimerText();
    }

    private IEnumerator UpdateTimeCoroutine()
    {
        while (gameRunning)
        {
            yield return null;
        }

        // Game has stopped, calculate high score
        CheckAndUpdateHighScore();

        // Activate save and restart buttons
        saveButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    private void UpdateTimerText()
    {
        timerText.text = $"Time: {Mathf.Round(gameTime)}s";
    }

    private void CheckAndUpdateHighScore()
    {
        if (gameTime < highScore || highScore == 0f)
        {
            highScore = gameTime;
            UpdateHighScoreText();
            SaveHighScore();
        }
    }

    private void UpdateHighScoreText()
    {
        highScoreText.text = $"Highscore: {Mathf.Round(highScore)}s";
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetFloat("HighScore", highScore);
        PlayerPrefs.Save();
    }

    private void LoadHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetFloat("HighScore");
        }
    }

    public void SaveHighScoreAndRestart()
    {
        SaveHighScore();
        RestartGame();
    }

    private void RestartGame()
    {
        // Reset game state
        gameTime = 0f;
        gameRunning = true;
        saveButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    public void MainCharacterDestroyed()
    {
        gameRunning = false;
        // Optionally, you can call CheckAndUpdateHighScore() here as well.
        CheckAndUpdateHighScore(); // Update high score immediately on game over
    }
}
