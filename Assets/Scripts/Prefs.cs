using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public static class Prefs
{
    public static int score
    {
        get => PlayerPrefs.GetInt(GameConsts.Score, 0);
        set
        {
            int curScore = PlayerPrefs.GetInt(GameConsts.Score, 0);
            if (value > curScore)
            {
                PlayerPrefs.SetInt(GameConsts.Score, value);
            }
        }
    }
}
