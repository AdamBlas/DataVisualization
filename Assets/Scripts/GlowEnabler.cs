using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GlowEnabler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject objectToToggle;

    public void OnPointerEnter(PointerEventData eventData)
    {
        objectToToggle.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        objectToToggle.SetActive(false);
    }
}
