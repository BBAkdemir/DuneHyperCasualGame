using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

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

    public State(Transform _player)
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
    public bool CanSeePlayer()
    {
        if (ClickControl.Instance.pressed == true)
        {
            return true;
        }
        return false;
    }
    public bool CanNotSeePlayer()
    {
        if (ClickControl.Instance.pressed == false)
        {
            return true;
        }
        return false;
    }
    public bool CanAttackPlayer()
    {
        if (Vector3.Distance(PatrolPointsCheck.Instance.gameObject.transform.position, AI.Instance.player.position) < 3)
        {
            return true;
        }
        return false;
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
        if (CanSeePlayer())
        {
            nextState = new Pursue(player);
            stage = EVENT.EXIT;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}

public class Pursue : State
{
    private int currentWayPoint = 0;
    public Pursue(Transform _player)
                : base(_player)
    {
        s_name = STATE.PURSUE;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        if (ClickControl.Instance.pressed == true)
        {
            PatrolPointsCheck.Instance.gameObject.transform.position = Vector3.MoveTowards(PatrolPointsCheck.Instance.gameObject.transform.position, AI.Instance.player.position, 2 * Time.deltaTime);
            PatrolPointsCheck.Instance.gameObject.transform.LookAt(AI.Instance.player.position);
        }
        else if (CanNotSeePlayer())
        {
            nextState = new Patrol(player);
            stage = EVENT.EXIT;
        }
        if (CanAttackPlayer())
        {
            nextState = new Attack(player);
            stage = EVENT.EXIT;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}

public class Attack : State
{
    private int currentWayPoint = 0;
    public Attack(Transform _player)
                : base(_player)
    {
        s_name = STATE.ATTACK;
    }

    public override void Enter()
    {
        PatrolPointsCheck.Instance.gameObject.transform.DOMoveY(-1,2f);
        //Buraya bizim karakterin damage yeem animasyonunu koyacaksýn
        nextState = new Idle(player);
        stage = EVENT.EXIT;
    }
    public override void Update()
    {
        base.Update();
    }
    public override void Exit()
    {
        base.Exit();
    }
}

public class Idle : State
{
    private int currentWayPoint = 0;
    public Idle(Transform _player)
                : base(_player)
    {
        s_name = STATE.IDLE;
    }

    public override void Enter()
    {
        
        DOVirtual.DelayedCall(2f, () => {
            nextState = new Patrol(player);
            stage = EVENT.EXIT;
        });
    }
    public override void Update()
    {
        base.Update();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
