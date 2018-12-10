using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    // panels
    public GameObject PanelWin, PanelLose, PanelCredits, PanelPause, Joystick, JoystickCanvas;
    public Text winText;
    public int enemyCount;
    public GameObject minimap;

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

        minimap = GameObject.Find("Minimap Parent");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("menu");
        Time.timeScale = 1f;
    }

    public void GoToGamePlay()
    {
        SceneManager.LoadScene("gameplay_backup");
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
        winText.text = "Parabéns, você salvou " + GameObject.Find("Player").GetComponent<Player>().money + " ovos de tartarugas!\n" + winText.text;
        PanelWin.SetActive(true);
        PanelLose.SetActive(false);
        JoystickCanvas.SetActive(false);
        minimap.SetActive(false);

    }

    public void OpenLoseScreen()
    {
        Time.timeScale = 1f;
        PanelWin.SetActive(false);
        PanelLose.SetActive(true);
        JoystickCanvas.SetActive(false);
        minimap.SetActive(false);
    }

    public void SetCreditsVisibility(bool visible)
    {
        PanelCredits.SetActive(visible);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PanelPause.SetActive(true);
        JoystickCanvas.SetActive(false);
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        PanelPause.SetActive(false);
        JoystickCanvas.SetActive(true);
    }
}