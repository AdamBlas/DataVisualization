using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomScrollView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scrollSensitivity;
    public bool mouseInside;

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseInside = true;
        print("enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseInside = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
