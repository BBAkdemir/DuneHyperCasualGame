using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWorm : MonoBehaviour
{
    public static AIWorm Instance;

    public Transform player;
    StateWorm currentState;
    bool isGameActive = true;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        currentState = new PatrolWorm(player);
    }

    void Update()
    {
        if (isGameActive)
            currentState = currentState.Process();
    }
}
