using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour
{

    [Header("Public Variable")]
    public bool _isGameOver = false;
    public bool _isCoOp_Mode = false;
    public static bool _isGamePaused = false;

    [Header("Private Variable")]
    //10 additional point for kill enemy 
    //20 additional point for hit asteroid
    [SerializeField] private int _points;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private string sceneName;
    private int _playerScore = 0;
    [SerializeField] private Player[] players;

    [SerializeField] private SpawnManager _spManager;
    [SerializeField] private PowerUpSpawner _powerUpManager;

    [Header("UI Element")]
    public GameObject mainGameContainer;
    public GameObject gameOverContainer;
    public GameObject quitGameContainer;
    public GameObject pauseGameContainer;
    public Text gameOverText;
    public Text highscoreText;
    public Text scoreText;
    public Image[] livesDisplay;
    public Text restartcomment;

    private void Awake()
    {

        Time.timeScale = 1f;
        gameOverContainer.SetActive(false);
        scoreText.text = "Score : " + 0;

        if (_spManager == null)
        {
            _spManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        }

        if (_powerUpManager == null)
        {
            _powerUpManager = GameObject.FindGameObjectWithTag("PowerUpSpawner").GetComponent<PowerUpSpawner>();
        }

        HideCursorMode(false);

    }

    void Update()
    {

#if UNITY_ANDROID
        AndroidController();
        restartcomment.text = "Press 'Fire' Button to Restart Game";
#elif UNITY_STANDALONE_WIN
        KeyboardController();
        restartcomment.text = "Press 'R' to Restart Game";
#endif

        HighScore();

        if (_isCoOp_Mode == true)
        {

            AreAllPlayerDie();

        }

        if (_isGameOver == true)
        {

            PlayerIsDead();
            OnGameOver();

        }

    }

#region Controller

    void AndroidController()
    {

        if (CrossPlatformInputManager.GetButton("Fire") && _isGameOver == true)
        {

            LoadSceneFunction(sceneName);

        }

    }

    void KeyboardController()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            PauseGame();
            //show the Pause Menu UI

        }

        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {

            LoadSceneFunction(sceneName);

        }

    }

#endregion

#region PauseMechanic

    public void PauseGame()
    {

        _isGamePaused = !_isGamePaused;
        PauseGameUISystem();

    }

    #region Cursor Hide Mode

    void HideCursorMode(bool _isVisible)
    {
        Cursor.visible = _isVisible;
        if (_isVisible)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    #endregion

    void PauseGameUISystem()
    {

        if (_isGamePaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        HideCursorMode(_isGamePaused);
        pauseGameContainer.SetActive(_isGamePaused);

    }

#endregion

#region ScoringSystem

    public void AddScore(int additionalScore)
    {

        _points = additionalScore;
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

        if (_isCoOp_Mode == true)
        {

            int _highScore = PlayerPrefs.GetInt("Co-Op_HighScore");
            if (_playerScore >= _highScore)
            {

                PlayerPrefs.SetInt("Co-Op_HighScore", _playerScore);
                
            }
            highscoreText.text = "HighScore = " + _highScore.ToString();

        }
        else
        {

            int _highScore = PlayerPrefs.GetInt("HighScore");
            if (_playerScore >= _highScore)
            {

                PlayerPrefs.SetInt("HighScore", _playerScore);

            }
            highscoreText.text = "HighScore = " + _highScore.ToString();

        }

    }

#endregion

#region PlayerLiveIndicator

    void PlayerIsDead()
    {

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

    public void PlayerLiveIndicator(int playerHp, int index)
    {

        livesDisplay[index].sprite = _liveSprites[playerHp];

    }

    void AreAllPlayerDie()
    {

        if (players[0]._isPlayerDefeat == true && players[1]._isPlayerDefeat == true)
        {
            _isGameOver = true;
        }
        else
        {
            _isGameOver = false;
        }

    }

    void OnGameOver()
    {
        //communicated with spawn manager 
        //let them know to stop running
        _spManager.OnPlayerDead();
        _powerUpManager.OnPlayerDead();

    }

    #endregion

#region LoadScenes

    public void LoadSceneFunction(string name)
    {

        SceneManager.LoadScene(name);

    }

    #endregion

#region Pause Menu and Exit Popup

    public void ShowPopUpQuitGame()
    {

        pauseGameContainer.SetActive(false);
        quitGameContainer.SetActive(true);

    }

    public void CancelQuit()
    {

        pauseGameContainer.SetActive(true);
        quitGameContainer.SetActive(false);

    }

    public void ExitGame()
    {

        Application.Quit();

    }

#endregion

}
