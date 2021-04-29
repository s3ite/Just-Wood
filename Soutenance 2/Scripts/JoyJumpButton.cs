using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyJumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isJumpPressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isJumpPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //isJumpPressed = false;
    }
}
