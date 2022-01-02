using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearch : MonoBehaviour
{
    [SerializeField] Vector2 showPosition;
    [SerializeField] Vector2 hidePosition;
    [SerializeField] float speed;

    public Coroutine showCoroutine;
    public Coroutine hideCoroutine;
    RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void OnMouseEnter()
    {
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);
        showCoroutine = StartCoroutine(Show());
    }
    private void OnMouseExit()
    {
        if (showCoroutine != null)
            StopCoroutine(showCoroutine);
        hideCoroutine = StartCoroutine(Hide());
    }

    IEnumerator Show()
    {
        while (transform.localPosition.x > showPosition.x)
        {
            GetComponent<RectTransform>().anchoredPosition += (transform.localPosition - showPosition) * speed * Time.deltaTime;
            transform.localPosition += (transform.localPosition - showPosition) * speed * Time.deltaTime;
            yield return null;
        }
        transform.localPosition = showPosition;
    }
    IEnumerator Hide()
    {
        while (transform.localPosition.x < hidePosition.x)
        {
            transform.localPosition -= (transform.localPosition - hidePosition) * speed * Time.deltaTime;
            yield return null;
        }
        transform.localPosition = hidePosition;
    }
}
