using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool gameLost = false;
    public bool gameWin = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void GameWin()
    {
        Time.timeScale = 0;
        ////içini doldur
    }
}
