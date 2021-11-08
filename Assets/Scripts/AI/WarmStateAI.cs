using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmStateAI : MonoBehaviour
{
    public enum States { Idle, Patrol, Dead }
    public States currentState;
    public Transform[] wayPoints;
    private int currentWayPoint = 0;

    void Update()
    {
        UpdateStates();
    }
    private void UpdateStates()
    {
        switch (currentState)
        {
            case States.Patrol:
                Patrol();
                break;
        }
    }
    void Patrol()
    {
        if (transform.position != wayPoints[currentWayPoint].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWayPoint].position, 2 * Time.deltaTime);
            transform.LookAt(wayPoints[currentWayPoint].position);
        }
        if (Vector3.Distance(transform.position, wayPoints[currentWayPoint].position) < 0.1f)
        {
            currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;
        }
    }
}
