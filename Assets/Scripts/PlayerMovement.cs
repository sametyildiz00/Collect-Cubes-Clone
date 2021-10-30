using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] RectTransform rect;
    [SerializeField] Text touchToStart;
    private Vector3 direction;
    Vector2 firstPos, targetPos;
    void Initialized()
    {
        EventManager.PointerDragged += Move;
        EventManager.PointerUpped += Stop;
        EventManager.PointerDowned += Touched;
    }
    private void Awake()
    {
        Initialized();
        rb = GetComponent<Rigidbody>();
    }
    public void Move(PointerEventData pointerEventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, pointerEventData.position, pointerEventData.pressEventCamera, out targetPos);
        var dragPos = targetPos - firstPos;
        direction = new Vector3(dragPos.x / Screen.width, 0, dragPos.y / Screen.height);

        if (pointerEventData.dragging)
        {
            rb.velocity = direction * speed;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }
    }
    public void Stop(PointerEventData pointerEventData)
    {
        rb.velocity = Vector3.zero;
    }
    public void Touched(PointerEventData pointerEventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, pointerEventData.position, pointerEventData.pressEventCamera, out firstPos);
        touchToStart.enabled = false;
    }

}
