using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AIRandomMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public int wayPointsCount;

    public GameObject finishPoint;
    public List<Vector3> wayPoints;
    public List<Vector3> wayPointsSpare;
    float distance;
    Vector3 pos;
    float randomLeftRight;

    private void Awake()
    {
        wayPoints = new List<Vector3>();
        wayPointsSpare = new List<Vector3>();
        wayPointsMachine(wayPointsCount);
    }
    void Start()
    {
        StartCoroutine(MoveWayPoints());
    }
    public void wayPointsMaker(Vector3 StartPos, int wayPointsCount)
    {
        distance = Vector3.Distance(StartPos, finishPoint.transform.position) / wayPointsCount;
        randomLeftRight = Random.Range(-Mathf.Abs(StartPos.z - finishPoint.transform.position.z), Mathf.Abs(StartPos.z - finishPoint.transform.position.z));
        pos = new Vector3(StartPos.x + distance, StartPos.y, StartPos.z + randomLeftRight);
        wayPoints.Add(pos);
    }
    public void wayPointsMachine(int wayPointsCount)
    {
        for (int i = 0; i <= wayPointsCount - 1; i++)
        {
            if (i == 0)
                wayPointsMaker(gameObject.transform.position, wayPointsCount);
            if (i > 0 && i < wayPointsCount - 1)
                wayPointsMaker(wayPoints[wayPoints.Count - 1], wayPointsCount - i);
            if (i == wayPointsCount - 1)
            {
                wayPoints.Add(finishPoint.transform.position);
                break;
            }
        }
    }
    public IEnumerator MoveWayPoints()
    {
        while (wayPoints.Count > 0)
        {
            if (transform.position != wayPoints[0])
                transform.position = Vector3.MoveTowards(transform.position, wayPoints[0], moveSpeed * Time.deltaTime);
            else
            {
                if (wayPoints.Count > 1)
                {
                    wayPointsSpare = new List<Vector3>();

                    for (int i = 1; i <= wayPoints.Count - 1; i++)
                        wayPointsSpare.Add(wayPoints[i]);

                    wayPoints = new List<Vector3>();

                    for (int i = 0; i <= wayPointsSpare.Count - 1; i++)
                        wayPoints.Add(wayPointsSpare[i]);
                }
                else
                    wayPoints = new List<Vector3>();
            }
            yield return null;
        }
    }
}
