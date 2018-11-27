using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    // panels
    public GameObject PanelWin, PanelLose, PanelCredits, Joystick;
    public int enemyCount;

    public void Update()
    {
        if (enemyCount <= 0)
        {
            // OpenWinScreen();
        
        }

        if (PanelLose == null)
        {
            PanelLose = GameObject.Find("PanelLose");
        }

        if (PanelWin == null)
        {
            PanelWin = GameObject.Find("PanelWin");
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("menu");
        Time.timeScale = 1f;
    }

    public void GoToGamePlay()
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
        Joystick.SetActive(false);
    }

    public void OpenLoseScreen()
    {
        Time.timeScale = 1f;
        PanelWin.SetActive(false);
        PanelLose.SetActive(true);
        Joystick.SetActive(false);

    }

    public void SetCreditsVisibility(bool visible)
    {
        PanelCredits.SetActive(visible);
    }
}