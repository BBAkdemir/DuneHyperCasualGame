using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State : MonoBehaviour
{
    public enum STATE
    {
        IDLE, PATROL, PURSUE, ATTACK, SLEEP, RUNAWAY
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE s_name;
    protected EVENT stage;
    protected Transform player;
    protected State nextState;

    float visDist = 20.0f;
    float visAngle = 30.0f;
    float shootDist = 15.0f;

    public State( Transform _player)
    {
        stage = EVENT.ENTER;
        player = _player;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
}

public class Patrol : State
{
    private int currentWayPoint = 0;
    public Patrol(Transform _player)
                : base(_player)
    {
        s_name = STATE.PATROL;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        if (PatrolPointsCheck.Instance.gameObject.transform.position != PatrolPointsCheck.Instance.PatrolPoints[currentWayPoint].transform.position)
        {
            PatrolPointsCheck.Instance.gameObject.transform.position = Vector3.MoveTowards(PatrolPointsCheck.Instance.gameObject.transform.position, PatrolPointsCheck.Instance.PatrolPoints[currentWayPoint].transform.position, 2 * Time.deltaTime);
            PatrolPointsCheck.Instance.gameObject.transform.LookAt(PatrolPointsCheck.Instance.PatrolPoints[currentWayPoint].transform.position);
        }
        if (Vector3.Distance(PatrolPointsCheck.Instance.gameObject.transform.position, PatrolPointsCheck.Instance.PatrolPoints[currentWayPoint].transform.position) < 0.1f)
        {
            currentWayPoint = (currentWayPoint + 1) % PatrolPointsCheck.Instance.PatrolPoints.Count;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
