using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Transform player;
    State currentState;
    bool isGameActive = true;

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
