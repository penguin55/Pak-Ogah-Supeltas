using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInGameManager : MonoBehaviour
{
    public GameObject pausePanel;
    public FadeManager fade;

    private bool toggle = false;

    public void Pause()
    {
        GameVariables._pauseGame = !GameVariables._pauseGame;
        pausePanel.SetActive(GameVariables._pauseGame);
    }

    public void Retry()
    {
        if (toggle) return;
        toggle = true;
        GameVariables._pauseGame = true;
        fade.FadeOut(SceneManager.GetActiveScene().name);
    }

    public void Home()
    {
        if (toggle) return;
        toggle = true;
        GameVariables._pauseGame = true;
        fade.FadeOut("MAIN_MENU");
    }

    public void NextLevel()
    {
        if (toggle) return;
        toggle = true;
        GameVariables._pauseGame = true;
        LevelManagement.levelCurrent++;
        fade.FadeOut(("Level "+(SceneManager.GetActiveScene().buildIndex+1)));
    }

    public void SoundClick()
    {
        AudioManager._audioManager.Play("Click");
    }
}
