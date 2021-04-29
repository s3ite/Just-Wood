using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyCrouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isCrouchPressed;
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
        isCrouchPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isCrouchPressed = false;
    }
}
