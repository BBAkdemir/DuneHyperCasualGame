using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class AIRandomMovement : MonoBehaviour
{
    public float moveSpeed;
    public int wayPointsCount;

    public GameObject finishPoint;
    public List<Vector3> wayPoints;
    public List<Vector3> wayPointsSpare;
    float distance;
    Vector3 pos;
    float randomLeftRight;
    bool wayPointAdded = false;

    NavMeshPath navPath;
    private NavMeshAgent ArtificialIntelligence;

    private void Awake()
    {
        ArtificialIntelligence = gameObject.GetComponent<NavMeshAgent>();
        wayPoints = new List<Vector3>();
        wayPointsSpare = new List<Vector3>();
        wayPointsMachine(wayPointsCount);
        moveSpeed = gameObject.GetComponent<Character>().Speed;
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

        #region Gidilemeyecek Yerlere Basmamamýzý Saðlayan Kod
        navPath = new NavMeshPath();
        ArtificialIntelligence.CalculatePath(pos, navPath);
        if (navPath.status == NavMeshPathStatus.PathComplete)
        {
            wayPoints.Add(pos);
            wayPointAdded = true;
        }
        #endregion
    }
    public void wayPointsMachine(int wayPointsCount)
    {
        for (int i = 0; i <= wayPointsCount - 1; i++)
        {
            if (i == 0)
            {
                wayPointsMaker(gameObject.transform.position, wayPointsCount);
                if (wayPointAdded == false)
                    i -= 1;
            }
            if (i > 0 && i < wayPointsCount - 1)
            {
                wayPointsMaker(wayPoints[wayPoints.Count - 1], wayPointsCount - i);
                if (wayPointAdded == false)
                    i -= 1;
            }
            if (i == wayPointsCount - 1)
            {
                wayPoints.Add(finishPoint.transform.position);
                if (wayPointAdded == false)
                    i -= 1;
                break;
            }
        }
    }
    public IEnumerator MoveWayPoints()
    {
        while (wayPoints.Count > 0)
        {
            if (Vector3.Distance(transform.position, new Vector3(wayPoints[0].x, gameObject.transform.position.y, wayPoints[0].z)) >= 0.2f)
            {
                if (gameObject.GetComponent<Character>().hareket == true)
                {
                    ArtificialIntelligence.isStopped = false;
                    ArtificialIntelligence.speed = moveSpeed;
                    ArtificialIntelligence.destination = new Vector3(wayPoints[0].x, gameObject.transform.position.y, wayPoints[0].z);
                }
                else
                {
                    ArtificialIntelligence.isStopped = true;
                }
            }
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
