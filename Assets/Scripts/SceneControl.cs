using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    // panels
    public GameObject PanelWin, PanelLose;

    public void GoToMenu()
    {
        SceneManager.LoadScene("menu");
        Time.timeScale = 1f;
    }

    public void GoToMenuGamePlay()
    {
        SceneManager.LoadScene("gameplay");
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void OpenWinScreen()
    {
        Time.timeScale = 1f;
        PanelWin.SetActive(true);
        PanelLose.SetActive(false);
    }

    public void OpenLoseScreen()
    {
        Time.timeScale = 1f;
        PanelWin.SetActive(false);
        PanelLose.SetActive(true);

    }
}