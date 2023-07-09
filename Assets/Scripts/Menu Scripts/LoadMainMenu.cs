using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
