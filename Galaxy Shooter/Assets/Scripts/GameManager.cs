using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static bool _isGamePaused = false;

    [Header("Private Variable")]
    private int _playerScore = 0;
    [SerializeField] private int _points = 10;
    [SerializeField] private Sprite[] _liveSprites;

    [Header("UI Element")]
    public GameObject mainGameContainer;
    public GameObject gameOverContainer;
    public Text gameOverText;
    public Text highscoreText;
    public Text scoreText;
    public Image livesDisplay;

    private void Awake()
    {

        gameOverContainer.SetActive(false);
        scoreText.text = "Score : " + 0;
        livesDisplay.sprite = _liveSprites[3];

    }

    void Update()
    {

        KeyboardController();

        HighScore();


    }

    void KeyboardController()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            PauseController();
            //show the Pause Menu UI

        }

    }

    #region PauseMechanic

    public void PauseController()
    {

        _isGamePaused = !_isGamePaused;
        PauseGame();

    }

    void PauseGame()
    {

        if (_isGamePaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }

    }

    #endregion

    #region ScoringSystem

    public void AddScore()
    {

        _playerScore += _points;
        Debug.Log("Player Score : " + _playerScore);
        ShowScore(_playerScore);

    }

    void ShowScore(int playerScore)
    {

        scoreText.text = "Score : " + playerScore.ToString();

    }

    void HighScore()
    {
        
        int _highScore = PlayerPrefs.GetInt("HighScore");
        Debug.Log("HighScore = " + _highScore);

        if (_playerScore >= _highScore)
        {

            PlayerPrefs.SetInt("HighScore", _playerScore);
            highscoreText.text = "HighScore = " + _highScore.ToString();

        }

    }

    #endregion

    #region PlayerLiveIndicator

    public void PlayerDeadIndicator()
    {
        //when player dead 
        //show the lives display to 0
        livesDisplay.sprite = _liveSprites[0];
        //show gameover panel
        gameOverContainer.SetActive(true);
        StartCoroutine(GameOverTextAnimation());
        //pause the game

    }

    IEnumerator GameOverTextAnimation()
    {

        while (true)
        {

            gameOverText.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            yield return new WaitForSeconds(.5f);
        }

    }

    public void PlayerLiveIndicator(int playerHp)
    {

        livesDisplay.sprite = _liveSprites[playerHp];

    }

    #endregion

}
