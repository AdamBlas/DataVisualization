using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TopChampIcon : MonoBehaviour
{
    public static Text label;
    [SerializeField] Text scoreText;

    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public Color startColor;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
        scoreText.enabled = false;
    }

    public void InitScore(int score, int topScore)
    {
        scoreText.text = score.ToString() + "pts";

        float scoreDependent = (float)score / topScore;

        if (scoreDependent < 0.35f)
        {
            scoreText.transform.position += new Vector3(-4, 0, 0);
            scoreText.alignment = TextAnchor.MiddleRight;
        }
    }

    private void OnMouseEnter()
    {
        scoreText.enabled = true;
    }

    private void OnMouseExit()
    {
        scoreText.enabled = false;
    }
}
