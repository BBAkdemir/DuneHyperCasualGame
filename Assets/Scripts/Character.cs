using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character Instance;

    public float Speed;
    public int Heart;
    public bool SafeZoneActive = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}