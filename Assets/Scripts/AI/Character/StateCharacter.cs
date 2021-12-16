using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCharacter : MonoBehaviour
{
    public enum STATECHARACTER
    {
        IDLE, RUN, SAFEZONE, DAMAGE
    };

    public enum EVENTCHARACTER
    {
        ENTER, UPDATE, EXIT
    };

    public STATECHARACTER s_name;
    protected EVENTCHARACTER stage;
    protected Transform player;
    protected StateCharacter nextState;

    public StateCharacter(Transform _player)
    {
        stage = EVENTCHARACTER.ENTER;
        player = _player;
    }

    public virtual void Enter() { stage = EVENTCHARACTER.UPDATE; }
    public virtual void Update() { stage = EVENTCHARACTER.UPDATE; }
    public virtual void Exit() { stage = EVENTCHARACTER.EXIT; }

    public StateCharacter Process()
    {
        if (stage == EVENTCHARACTER.ENTER) Enter();
        if (stage == EVENTCHARACTER.UPDATE) Update();
        if (stage == EVENTCHARACTER.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
    public bool CharacterIdle()
     {
         if (ClickControl.Instance.pressed == false)
         {
             return true;
         }
         return false;
     }
     public bool CharacterRun()
     {
         if (ClickControl.Instance.pressed == true)
         {
             return true;
         }
         return false;
     }
    public bool CharacterSafeZoneActive()
    {
        if (LevelManager.Instance.PlayerObject.GetComponent<Character>().SafeZoneActive == true)
        {
            return true;
        }
        return false;
    }
    public bool CharacterDamage()
     {
         if (LevelManager.Instance.PlayerObject.GetComponent<Character>().DamageActive == true)
         {
             return true;
         }
         return false;
     }
}
public class IdleCharacter : StateCharacter
{
    public IdleCharacter(Transform _player)
                : base(_player)
    {
        s_name = STATECHARACTER.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        if (CharacterRun())
        {
            LevelManager.Instance.PlayerObject.GetComponent<Character>().hareket = true;
            nextState = new RunCharacter(player);
            stage = EVENTCHARACTER.EXIT;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}

public class RunCharacter : StateCharacter
{
    public RunCharacter(Transform _player)
                : base(_player)
    {
        s_name = STATECHARACTER.RUN;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        if (CharacterIdle())
        {
            LevelManager.Instance.PlayerObject.GetComponent<Character>().hareket = false;
            nextState = new IdleCharacter(player);
            stage = EVENTCHARACTER.EXIT;
        }
        if (CharacterSafeZoneActive())
        {
            LevelManager.Instance.PlayerObject.GetComponent<Character>().SafeZoneActive = true;
            nextState = new SafeZoneCharacter(player);
            stage = EVENTCHARACTER.EXIT;
        }
        if (CharacterDamage())
        {
            LevelManager.Instance.PlayerObject.GetComponent<Character>().DamageActive = true;
            LevelManager.Instance.PlayerObject.GetComponent<Character>().hareket = false;
            ClickControl.Instance.ControlActivate = false;
            nextState = new DamageCharacter(player);
            stage = EVENTCHARACTER.EXIT;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
public class SafeZoneCharacter : StateCharacter
{
    public SafeZoneCharacter(Transform _player)
                : base(_player)
    {
        s_name = STATECHARACTER.SAFEZONE;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        if (CharacterSafeZoneActive() == false && CharacterRun())
        {
            LevelManager.Instance.PlayerObject.GetComponent<Character>().hareket = true;
            nextState = new RunCharacter(player);
            stage = EVENTCHARACTER.EXIT;
        }
        if (CharacterIdle())
        {
            LevelManager.Instance.PlayerObject.GetComponent<Character>().hareket = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
public class DamageCharacter : StateCharacter
{
    public DamageCharacter(Transform _player)
                : base(_player)
    {
        s_name = STATECHARACTER.DAMAGE;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        if (CharacterRun())
        {
            LevelManager.Instance.PlayerObject.GetComponent<Character>().hareket = true;
            ClickControl.Instance.ControlActivate = true;
            nextState = new RunCharacter(player);
            stage = EVENTCHARACTER.EXIT;
        }
        if (CharacterIdle())
        {
            LevelManager.Instance.PlayerObject.GetComponent<Character>().hareket = false;
            ClickControl.Instance.ControlActivate = true;
            nextState = new IdleCharacter(player);
            stage = EVENTCHARACTER.EXIT;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
