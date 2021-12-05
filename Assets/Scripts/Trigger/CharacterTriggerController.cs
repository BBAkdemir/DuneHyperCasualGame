using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTriggerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ICollectable>() == null) return;
        else
        {
            other.GetComponent<ICollectable>().Collect();
        }
    }
    public void CollectSpeed(float SpeedAdd, GameObject gameObject)
    {
        ClickControl.Instance.speed += SpeedAdd;
        Destroy(gameObject);
    }
}
