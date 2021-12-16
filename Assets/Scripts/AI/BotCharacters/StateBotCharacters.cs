using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBotCharacters : MonoBehaviour
{
    public enum STATEBOTCHARACTERS
    {
        IDLE, RUN, SAFEZONE, DAMAGE
    };

    public enum EVENTBOTCHARACTERS
    {
        ENTER, UPDATE, EXIT
    };

    public STATEBOTCHARACTERS s_name;
    protected EVENTBOTCHARACTERS stage;
    protected Transform player;
    protected StateBotCharacters nextState;
    public LevelManager levelManager;
    public List<GameObject> nearBoosters;

    public StateBotCharacters(Transform _player)
    {
        stage = EVENTBOTCHARACTERS.ENTER;
        player = _player;
    }

    public virtual void Enter() { stage = EVENTBOTCHARACTERS.UPDATE; }
    public virtual void Update() { stage = EVENTBOTCHARACTERS.UPDATE; }
    public virtual void Exit() { stage = EVENTBOTCHARACTERS.EXIT; }

    public StateBotCharacters Process()
    {
        if (stage == EVENTBOTCHARACTERS.ENTER) Enter();
        if (stage == EVENTBOTCHARACTERS.UPDATE) Update();
        if (stage == EVENTBOTCHARACTERS.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
    private void Start()
    {
        levelManager = GameManager.Instance.levelManager.GetComponent<LevelManager>();
        StartCoroutine(ControlBoosters());
    }
    public IEnumerator ControlBoosters()
    {
        while (true)
        {
            foreach (var item in levelManager.Boosters)
            {
                if (Vector3.Distance(item.transform.position, transform.position) < 10)
                {
                    nearBoosters.Add(item);
                    levelManager.reserveBooster = item;
                    if (levelManager.chosenAI != null && Vector3.Distance(levelManager.reserveBooster.transform.position, levelManager.worm.transform.position) < Vector3.Distance(levelManager.chosenAI.transform.position, levelManager.worm.transform.position))
                    {
                        levelManager.chosenAI = levelManager.reserveBooster;
                    }
                    if (levelManager.chosenAI == null)
                    {
                        levelManager.chosenAI = levelManager.reserveBooster;
                    }
                }
            }
            yield return new WaitForSeconds(2f);
            nearBoosters = new List<GameObject>();
        }
    }
    public bool BotCharacterIdle()
    {
        if (Vector3.Distance(LevelManager.Instance.worm.transform.position, player.transform.position) < 3)
        {
            return true;
        }
        return false;
    }
    public bool BotCharacterRun()
    {
        if (Vector3.Distance(LevelManager.Instance.worm.transform.position, player.transform.position) > 15)
        {
            return true;
        }
        return false;
    }
    public bool BotCharacterBoost()
    {
        if (levelManager.chosenBooster != null)
        {
            return true;
        }
        return false;
    }
    //public bool CharacterSafeZone()
    //{
    //    if (true)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
    //public bool CharacterDamage()
    //{
    //    if (true)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
}
public class IdleAICharacters : StateBotCharacters
{
    public IdleAICharacters(Transform _player)
                : base(_player)
    {
        s_name = STATEBOTCHARACTERS.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        if (BotCharacterRun())
        {
            player.GetComponent<Character>().hareket = true;
            nextState = new RunAICharacters(player);
            stage = EVENTBOTCHARACTERS.EXIT;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}

public class RunAICharacters : StateBotCharacters
{
    public RunAICharacters(Transform _player)
                : base(_player)
    {
        s_name = STATEBOTCHARACTERS.RUN;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        if (BotCharacterIdle())
        {
            player.GetComponent<Character>().hareket = false;
            nextState = new IdleAICharacters(player);
            stage = EVENTBOTCHARACTERS.EXIT;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
//public class SafeZoneAICharacters : StateBotCharacters
//{
//    public SafeZoneAICharacters(Transform _player)
//                : base(_player)
//    {
//        s_name = STATEBOTCHARACTERS.SAFEZONE;
//    }

//    public override void Enter()
//    {
//        base.Enter();
//    }
//    public override void Update()
//    {
//        base.Update();
//    }
//    public override void Exit()
//    {
//        base.Exit();
//    }
//}
//public class DamageAICharacters : StateBotCharacters
//{
//    public DamageAICharacters(Transform _player)
//                : base(_player)
//    {
//        s_name = STATEBOTCHARACTERS.DAMAGE;
//    }

//    public override void Enter()
//    {
//        base.Enter();
//    }
//    public override void Update()
//    {
//        base.Update();
//    }
//    public override void Exit()
//    {
//        base.Exit();
//    }
//}
