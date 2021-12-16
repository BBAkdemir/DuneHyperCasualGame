using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterTriggerController : MonoBehaviour
{
    public GameObject characterObj;
    //NavMeshAgent characterNavMesh;
    Character characterScript;
    AIRandomMovement randomMovementScript;
    private void Start()
    {
        characterScript = characterObj.GetComponent<Character>();
        //characterNavMesh = characterObj.GetComponent<NavMeshAgent>();
        randomMovementScript = characterObj.GetComponent<AIRandomMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ICollectable>() == null) return;
        else
        {
            other.GetComponent<ICollectable>().Collect(characterObj);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ExitSafeZone();
    }
    public void CollectSpeed(float SpeedAdd, GameObject gameObject)
    {
        characterScript.Speed += SpeedAdd;
        if (characterScript.objectType == Character.ObjectType.Player)
        {
            ClickControl.Instance.speed += SpeedAdd;
        }
        if (characterScript.objectType == Character.ObjectType.AI)
        {
            randomMovementScript.moveSpeed += SpeedAdd;
        }
        Destroy(gameObject);
    }

    public void EnterSafeZone(float TimeAdd, GameObject gameObject)
    {
        if (gameObject.GetComponent<TrueFalseControl>().Control == false)
        {
            GameManager.Instance.levelManager.GetComponent<LevelManager>().levelTime += TimeAdd;
            gameObject.GetComponent<TrueFalseControl>().Control = true;
        }
        characterScript.SafeZoneActive = true;
    }
    public void ExitSafeZone()
    {
        characterScript.SafeZoneActive = false;
    }
    public void EnterGameFinish()
    {
        if (characterObj == LevelManager.Instance.PlayerObject)
        {
            GameManager.Instance.gameWin = true;
            GameManager.Instance.GameWin();
        }
    }
}
