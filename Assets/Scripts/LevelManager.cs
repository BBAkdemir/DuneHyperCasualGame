using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public float levelTime;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}