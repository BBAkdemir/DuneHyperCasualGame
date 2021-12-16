using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public float levelTime;
    public GameObject worm;
    public GameObject chosenAI;
    public GameObject reserveAI;
    public GameObject chosenBooster;
    public GameObject reserveBooster;
    public GameObject PlayerObject;
    public List<GameObject> Characters;
    public List<GameObject> Boosters;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
