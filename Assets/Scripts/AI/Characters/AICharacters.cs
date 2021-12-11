using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacters : MonoBehaviour
{
    public Transform player;
    public StateCharacters currentState;
    bool isGameActive = true;

    void Start()
    {
        currentState = new RunAICharacters(player);
    }

    void Update()
    {
        if (isGameActive)
            currentState = currentState.Process();
    }
}
