using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuGameManager : MonoBehaviour
{

    public void ChangeScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);

    }

}
