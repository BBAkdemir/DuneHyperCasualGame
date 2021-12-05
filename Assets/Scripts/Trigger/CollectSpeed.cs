using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSpeed : MonoBehaviour, ICollectable
{
    [SerializeField] int SpeedAdd;
    public void Collect()
    {
        FindObjectOfType<CharacterTriggerController>().CollectSpeed(SpeedAdd, gameObject);
    }
}
