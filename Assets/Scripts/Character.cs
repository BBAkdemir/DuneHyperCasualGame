using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum ObjectType
    {
        Player, AI
    };
    public ObjectType objectType;
    public float Speed;
    public int Heart;
    public bool SafeZoneActive = false;
    public bool hareket = true;
    public bool DamageActive = false;

    public void DamageFinish()
    {
        LeanTween.delayedCall(gameObject, 2f, () =>
        {
            DamageActive = false;
        });
    }
}
