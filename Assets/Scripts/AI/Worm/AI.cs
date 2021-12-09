using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public static AI Instance;

    public Transform player;
    State currentState;
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
        currentState = new Patrol(player);
    }

    void Update()
    {
        if (isGameActive)
            currentState = currentState.Process();
    }
}
