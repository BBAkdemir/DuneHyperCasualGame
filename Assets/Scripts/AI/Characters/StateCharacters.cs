using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCharacters : MonoBehaviour
{
    public enum STATECHARACTERS
    {
        IDLE, RUN, RUNAWAY
    };

    public enum EVENTCHARACTERS
    {
        ENTER, UPDATE, EXIT
    };

    public STATECHARACTERS s_name;
    protected EVENTCHARACTERS stage;
    protected Transform player;
    protected StateCharacters nextState;

    public StateCharacters(Transform _player)
    {
        stage = EVENTCHARACTERS.ENTER;
        player = _player;
    }

    public virtual void Enter() { stage = EVENTCHARACTERS.UPDATE; }
    public virtual void Update() { stage = EVENTCHARACTERS.UPDATE; }
    public virtual void Exit() { stage = EVENTCHARACTERS.EXIT; }

    public StateCharacters Process()
    {
        if (stage == EVENTCHARACTERS.ENTER) Enter();
        if (stage == EVENTCHARACTERS.UPDATE) Update();
        if (stage == EVENTCHARACTERS.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
   /* public bool SnakeDangerLevel0()
    {
        if (true)
        {
            return true;
        }
        return false;
    }
    public bool SnakeDangerLevel1()
    {
        if (true)
        {
            return true;
        }
        return false;
    }
    public bool SnakeDangerLevel2()
    {
        if (true)
        {
            return true;
        }
        return false;
    }*/
}
public class IdleAICharacters : StateCharacters
{
    public IdleAICharacters(Transform _player)
                : base(_player)
    {
        s_name = STATECHARACTERS.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
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

public class RunAICharacters : StateCharacters
{
    public RunAICharacters(Transform _player)
                : base(_player)
    {
        s_name = STATECHARACTERS.RUN;
    }

    public override void Enter()
    {
        base.Enter();
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

public class RunAwayAICharacters : StateCharacters
{
    public RunAwayAICharacters(Transform _player)
                : base(_player)
    {
        s_name = STATECHARACTERS.RUNAWAY;
    }

    public override void Enter()
    {
        base.Enter();
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
