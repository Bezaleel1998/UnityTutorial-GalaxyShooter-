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
    private int _highScore = 0;
    private int _coopHS = 0;

    private void Update()
    {
        
        /// C# write to txt file newLine with \r\n
        _highScore = PlayerPrefs.GetInt("HighScore");
        _coopHS = PlayerPrefs.GetInt("Co-Op_HighScore");
        _highscoreText.text = "<color=lime>Highscore = " + _highScore.ToString() + "</color>\r\n" +
            "<color=blue>Co-Op Highscore = " + _coopHS.ToString() + "</color>";

    }

    public void LoadSceneFunction(string name)
    {

        SceneManager.LoadScene(name);

    }

}
