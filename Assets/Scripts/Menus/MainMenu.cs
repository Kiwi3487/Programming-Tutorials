using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Start");
    }
    
    public void ReadMes()
    {
        SceneManager.LoadScene("ReadMe");
    }
    
    public void MainMenus()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
