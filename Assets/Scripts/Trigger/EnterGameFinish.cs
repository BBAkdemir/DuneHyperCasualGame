using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGameFinish : MonoBehaviour, ICollectable
{
    public void Collect(GameObject gameObject)
    {
        gameObject.GetComponent<CharacterTriggerController>().EnterGameFinish();
    }
}
