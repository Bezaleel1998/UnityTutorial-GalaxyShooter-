using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuGameManager : MonoBehaviour
{

    [Header("UI")]
    [SerializeField]
    private Text _highscoreText;

    [Header("Variable")]
    private float _highScore = 0;

    private void Update()
    {
        
        _highScore = PlayerPrefs.GetInt("HighScore");
        _highscoreText.text = "Highscore = " + _highScore.ToString();

    }

    public void ChangeScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);

    }

}
