using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointsCheck : MonoBehaviour
{
    public static PatrolPointsCheck Instance;

    private List<GameObject> patrolPoints = new List<GameObject>();
    public List<GameObject> PatrolPoints { get { return patrolPoints; } }
    private void Awake()
    {
        PatrolPoints.AddRange(GameObject.FindGameObjectsWithTag("PatrolPoints"));
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
