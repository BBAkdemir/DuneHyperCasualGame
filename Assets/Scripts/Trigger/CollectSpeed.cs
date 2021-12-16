using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSpeed : MonoBehaviour, ICollectable
{
    [SerializeField] int SpeedAdd;
    public void Collect(GameObject gameObject)
    {
        gameObject.GetComponent<CharacterTriggerController>().CollectSpeed(SpeedAdd, this.gameObject);
    }
}
