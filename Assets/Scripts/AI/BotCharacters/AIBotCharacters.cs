using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBotCharacters : MonoBehaviour
{
    public Transform player;
    public StateBotCharacters currentState;
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
