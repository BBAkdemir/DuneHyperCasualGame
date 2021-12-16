using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : MonoBehaviour
{
    public Transform player;
    public StateCharacter currentState;
    bool isGameActive = true;

    void Start()
    {
        currentState = new IdleCharacter(player);
    }

    void Update()
    {
        if (isGameActive)
            currentState = currentState.Process();
    }
}
