using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class StateWorm : MonoBehaviour
{
    public enum STATEWORM
    {
        IDLE, PATROL, PURSUE, ATTACK, SLEEP, RUNAWAY
    };

    public enum EVENTWORM
    {
        ENTER, UPDATE, EXIT
    };
    public STATEWORM s_name;
    protected EVENTWORM stage;
    protected Transform player;
    protected StateWorm nextState;
    float visDist = 20.0f;
    float visAngle = 30.0f;
    float shootDist = 15.0f;
    public List<GameObject> RunStateCharacters;
    public LevelManager levelManager;
    public StateWorm(Transform _player)
    {
        stage = EVENTWORM.ENTER;
        player = _player;
    }

    public virtual void Enter() { stage = EVENTWORM.UPDATE; }
    public virtual void Update() { stage = EVENTWORM.UPDATE; }
    public virtual void Exit() { stage = EVENTWORM.EXIT; }
    public StateWorm Process()
    {
        if (stage == EVENTWORM.ENTER) Enter();
        if (stage == EVENTWORM.UPDATE) Update();
        if (stage == EVENTWORM.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
    private void Start()
    {
        levelManager = GameManager.Instance.levelManager.GetComponent<LevelManager>();
        StartCoroutine(ControlAI());
    }
    public IEnumerator ControlAI()
    {
        while (true)
        {
            foreach (var item in levelManager.Characters)
            {
                if (item.GetComponent<Character>().hareket == true)
                {
                    RunStateCharacters.Add(item);
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
            yield return new WaitForSeconds(1f);
            RunStateCharacters = new List<GameObject>();
        }
    }
    public bool CanSeePlayer()
    {
        if (levelManager.chosenAI != null && levelManager.chosenAI.GetComponent<Character>().hareket == true)
        {
            return true;
        }
        return false;
    }
    public bool CanNotSeePlayer()
    {
        if (/*levelManager.chosenAI == null || (levelManager.chosenAI != null && levelManager.chosenAI.GetComponent<Character>().hareket == false)*/ levelManager.chosenAI.GetComponent<Character>().hareket == false || levelManager.chosenAI.GetComponent<Character>().SafeZoneActive == true)
        {
            return true;
        }
        return false;
    }
    public bool CanAttackPlayer()
    {
        if (levelManager.chosenAI != null && Vector3.Distance(levelManager.worm.transform.position, levelManager.chosenAI.transform.position) < 2 && levelManager.chosenAI.transform.GetComponent<Character>().SafeZoneActive == false && levelManager.chosenAI.GetComponent<Character>().hareket == true)
        {
            return true;
        }
        return false;
    }
}

public class PatrolWorm : StateWorm
{
    private int currentWayPoint = 0;
    public PatrolWorm(Transform _player)
                : base(_player)
    {
        levelManager = GameManager.Instance.levelManager.GetComponent<LevelManager>();
        s_name = STATEWORM.PATROL;
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
        if (CanSeePlayer() && levelManager.chosenAI.transform.GetComponent<Character>().SafeZoneActive == false && levelManager.chosenAI.transform.GetComponent<Character>().hareket == true)
        {
            nextState = new PursueWorm(player);
            stage = EVENTWORM.EXIT;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}

public class PursueWorm : StateWorm
{
    private int currentWayPoint = 0;
    public PursueWorm(Transform _player)
                : base(_player)
    {
        levelManager = GameManager.Instance.levelManager.GetComponent<LevelManager>();
        s_name = STATEWORM.PURSUE;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        if (levelManager.chosenAI.transform.GetComponent<Character>().SafeZoneActive == false && levelManager.chosenAI.transform.GetComponent<Character>().hareket == true)
        {
            levelManager.worm.transform.position = Vector3.MoveTowards(levelManager.worm.transform.position, levelManager.chosenAI.transform.position, 2 * Time.deltaTime);
            levelManager.worm.transform.LookAt(levelManager.chosenAI.transform.position);
        }
        else if (CanNotSeePlayer())
        {
            nextState = new PatrolWorm(player);
            stage = EVENTWORM.EXIT;
        }
        if (CanAttackPlayer())
        {
            nextState = new AttackWorm(player);
            stage = EVENTWORM.EXIT;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}

public class AttackWorm : StateWorm
{
    private int currentWayPoint = 0;
    public AttackWorm(Transform _player)
                : base(_player)
    {
        levelManager = GameManager.Instance.levelManager.GetComponent<LevelManager>();
        s_name = STATEWORM.ATTACK;
    }

    public override void Enter()
    {
        levelManager.worm.transform.DOMoveY(-1, 2f);

        if (levelManager.chosenAI.transform.GetComponent<Character>().Heart > 1)
        {
            levelManager.chosenAI.transform.GetComponent<Character>().Heart -= 1;
            levelManager.chosenAI.transform.GetComponent<Character>().DamageActive = true;
            levelManager.chosenAI.transform.GetComponent<Character>().DamageFinish();
            levelManager.chosenAI = null;
        }
        else if (levelManager.chosenAI.transform.GetComponent<Character>().objectType == Character.ObjectType.AI)
        {
            levelManager.chosenAI.transform.GetComponent<Character>().Heart = 0;
            levelManager.Characters.Remove(levelManager.chosenAI);
            Destroy(levelManager.chosenAI);
            levelManager.chosenAI = null;

            //GameManager.Instance.gameLost = true;
            //Time.timeScale = 0;
            //burayada game managerde yapacaðýmýz metot çalýþtýrýlacak
        }
        else if (levelManager.chosenAI.transform.GetComponent<Character>().objectType == Character.ObjectType.Player)
        {
            levelManager.chosenAI.transform.GetComponent<Character>().Heart = 0;
            levelManager.Characters.Remove(levelManager.chosenAI);
            Destroy(levelManager.chosenAI);
            levelManager.chosenAI = null;
            GameManager.Instance.gameLost = true;
            Time.timeScale = 0;
            //burayada game managerde yapacaðýmýz metot çalýþtýrýlacak
        }

        //Buraya bizim karakterin damage yeem animasyonunu koyacaksýn
        nextState = new PatrolWorm(player);
        stage = EVENTWORM.EXIT;
    }
    public override void Update()
    {
        if (CanNotSeePlayer())
        {
            nextState = new PatrolWorm(player);
            stage = EVENTWORM.EXIT;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}

public class IdleWorm : StateWorm
{
    private int currentWayPoint = 0;
    public IdleWorm(Transform _player)
                : base(_player)
    {
        levelManager = GameManager.Instance.levelManager.GetComponent<LevelManager>();
        s_name = STATEWORM.IDLE;
    }

    public override void Enter()
    {
        DOVirtual.DelayedCall(2f, () =>
        {
            nextState = new PatrolWorm(player);
            stage = EVENTWORM.EXIT;
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

