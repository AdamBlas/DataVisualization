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
            Vector2 pos = new Vector2(transform.localPosition.x, transform.localPosition.y);
            rect.anchoredPosition -= (pos - showPosition) * speed * Time.deltaTime;
            yield return null;
        }
        transform.localPosition = showPosition;
    }
    IEnumerator Hide()
    {
        while (transform.localPosition.x < hidePosition.x)
        {
            Vector2 pos = new Vector2(transform.localPosition.x, transform.localPosition.y);
            rect.anchoredPosition -= (pos - hidePosition) * speed * Time.deltaTime;
            yield return null;
        }
        transform.localPosition = hidePosition;
    }
}
