using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickControl : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler /*IPointerEnterHandler*/
{
    public static ClickControl Instance;

    Vector2 firstClickPosition;
    Vector2 moveClickPosition;
    Vector2 currentSwipe;

    public float screenSensitive;//20
    public float speed;

    public GameObject player;

    public bool pressed = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        speed = player.GetComponent<Character>().Speed;
    }
    private void Update()
    {
        if (pressed == true)
        {
            //player.GetComponent<Rigidbody>().AddForce(player.transform.forward);
            player.transform.position += -player.transform.forward * speed * Time.deltaTime;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        firstClickPosition = eventData.pointerCurrentRaycast.screenPosition;
        pressed = true;
    }
    public void OnPointerMove(PointerEventData eventData)
    {
        if (pressed == true)
        {
            moveClickPosition = eventData.pointerCurrentRaycast.screenPosition;

            currentSwipe = firstClickPosition - moveClickPosition;
            currentSwipe.Normalize();

            Vector3 direction = player.transform.forward + new Vector3(currentSwipe.x, 0f, currentSwipe.y);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion newRot = Quaternion.Euler(0, targetAngle, 0);
            player.transform.rotation = newRot;
        }
    }
    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    if (pressed == true)
    //    {
    //        //player.GetComponent<Rigidbody>().AddForce(player.transform.forward);
    //        player.transform.position += player.transform.forward * speed * Time.deltaTime;
    //    }
    //}
    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
}
