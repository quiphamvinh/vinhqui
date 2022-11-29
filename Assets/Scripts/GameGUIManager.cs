using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject Main;
    public GameObject Game;

    public DiaLog GameDiaLog;
    public DiaLog PauseDiaLog;

    public Image fireRateFilled;
    public Text timerText;
    public Text killedCountingText;

    DiaLog curDiaLog;

    public DiaLog CurDiaLog { get => curDiaLog; set => curDiaLog = value; }
    public override void Awake()
    {
        MakeSingleton(false);
    }
    public void ShowGame(bool isShow)
    {
        if (Game)
        {
            Game.SetActive(isShow);
        }
        if (Main)
        {
            Main.SetActive(!isShow);
        }
    }
    public void UpdateTimer(string time)
    {
        if (timerText)
        {
            timerText.text = time;
        }

        
    }
    public void UpdateKilledCounting(int killed)
    {
        if (killedCountingText)
        {
            killedCountingText.text = "x" + killed.ToString();
        }
    }
    public void UpdateFireRate(float rate)
    {
        if (fireRateFilled)
        {
            fireRateFilled.fillAmount = rate;
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        if (PauseDiaLog)
        {
            PauseDiaLog.Show(true);
            PauseDiaLog.UpdateDialog("Pause", ": x" + Prefs.score);
            curDiaLog = PauseDiaLog;
        }
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (curDiaLog)
        {
            curDiaLog.Show(false);
        }
    }
    public void BackToHome()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Replay()
    {
        if (curDiaLog)
        {
            curDiaLog.Show(false);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void Exit()
    {
        Replay();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Application.Quit();
    }
}
