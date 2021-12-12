using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TopChampIcon : MonoBehaviour
{
    public static Text label;
    public int score;
    string topX = string.Empty;

    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public LineRenderer lineRenderer;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnMouseEnter()
    {
        if (string.IsNullOrWhiteSpace(topX))
            topX = label.text;

        label.text = score.ToString() + " pts";
    }

    private void OnMouseExit()
    {
        label.text = topX;
    }
}
