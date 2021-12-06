using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSafeZone : MonoBehaviour, ICollectable
{
    [SerializeField] int TimeAdd;
    public void Collect()
    {
        FindObjectOfType<CharacterTriggerController>().EnterSafeZone(TimeAdd, gameObject);
    }
}
