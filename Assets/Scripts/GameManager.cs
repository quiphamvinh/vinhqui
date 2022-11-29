using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Bird[] birdPrefabs;
    public float spawnTime;
    public int timeLimit;

    int curTimeLimit;
    int birdKilled;
    bool isGameOver;

    public bool IsGameover { get => isGameOver; set => isGameOver = value; }
    public int BirdKilled { get => birdKilled; set => birdKilled = value; }
    public int CurTimeLimit { get => curTimeLimit; set => curTimeLimit = value; }

    public override void Awake()
    {
        MakeSingleton(false);
        curTimeLimit = timeLimit;
    }
    public override void Start()
    {
        GameGUIManager.Ins.ShowGame(false);
        GameGUIManager.Ins.UpdateKilledCounting(birdKilled);

    }
    public void PlayGame()
    {
        StartCoroutine(GameSpawn());
        StartCoroutine(TimeCountDown());
        GameGUIManager.Ins.ShowGame(true);
    }
    IEnumerator TimeCountDown()
    {       
        while (curTimeLimit > 0)
        {
            yield return new WaitForSeconds(1f);
            curTimeLimit--;

            if (curTimeLimit <= 0)
            {
                isGameOver = true;
                if (birdKilled > Prefs.score)
                {
                    GameGUIManager.Ins.GameDiaLog.UpdateDialog("New Score", ": x" + birdKilled);
                }
                else if (birdKilled <= Prefs.score)
                {
                    GameGUIManager.Ins.GameDiaLog.UpdateDialog("Your Score", ": x" + birdKilled);
                }
                Prefs.score = birdKilled;
                GameGUIManager.Ins.GameDiaLog.Show(true);
                GameGUIManager.Ins.CurDiaLog = GameGUIManager.Ins.GameDiaLog;
            }
            GameGUIManager.Ins.UpdateTimer(IntToTime(curTimeLimit));
        }
    }
    IEnumerator GameSpawn()
    {
        while (!isGameOver)
        {
            SpawnBird();
            yield return new WaitForSeconds(spawnTime);
        }
    }
    void SpawnBird()
    {
        Vector3 spawnPos = Vector3.zero;
        float randCheck = Random.Range(0f, 1f);
        if (randCheck >= 0.5f)
        {
            spawnPos = new Vector3(12, Random.Range(-3f, 4f), 0);
        }
        else
        {
            spawnPos = new Vector3(-12, Random.Range(-3f, 4f), 0);
        }
        if (birdPrefabs != null && birdPrefabs.Length > 0)
        {
            int randIdx = Random.Range(0, birdPrefabs.Length);
            if (birdPrefabs[randIdx] != null)
            {
                Bird birdClone = Instantiate(birdPrefabs[randIdx], spawnPos, Quaternion.identity);
            }
        }
    }
    string IntToTime(int time)
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);
        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    
}
