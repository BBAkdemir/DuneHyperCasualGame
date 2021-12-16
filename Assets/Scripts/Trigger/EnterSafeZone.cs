using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSafeZone : MonoBehaviour, ICollectable
{
    [SerializeField] int TimeAdd;
    public void Collect(GameObject gameObject)
    {
        gameObject.GetComponent<CharacterTriggerController>().EnterSafeZone(TimeAdd, this.gameObject);
    }
}
