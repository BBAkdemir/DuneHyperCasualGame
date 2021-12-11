using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public float levelTime;
    public GameObject worm;
    public GameObject chosenAI;
    public GameObject kova;
    public List<GameObject> Characters;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
