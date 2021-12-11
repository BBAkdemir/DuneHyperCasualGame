using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTriggerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ICollectable>() == null) return;
        else
        {
            other.GetComponent<ICollectable>().Collect();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ExitSafeZone();
    }
    public void CollectSpeed(float SpeedAdd, GameObject gameObject)
    {
        ClickControl.Instance.speed += SpeedAdd;
        Destroy(gameObject);
    }

    public void EnterSafeZone(float TimeAdd, GameObject gameObject)
    {
        if (gameObject.GetComponent<TrueFalseControl>().Control == false)
        {
            GameManager.Instance.levelManager.GetComponent<LevelManager>().levelTime += TimeAdd;
            gameObject.GetComponent<TrueFalseControl>().Control = true;
        }
        ClickControl.Instance.player.GetComponent<Character>().SafeZoneActive = true;
    }
    public void ExitSafeZone()
    {
        ClickControl.Instance.player.GetComponent<Character>().SafeZoneActive = false;
    }
    public void EnterGameFinish(GameObject gameObject)
    {
        GameManager.Instance.gameWin = true;
        GameManager.Instance.GameWin();

    }
}
