using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TopChampIcon : MonoBehaviour
{
    public static Text label;
    [SerializeField] Text scoreText;
    public int score;
    string topX = string.Empty;

    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public Color startColor;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void InitScore(int score, int topScore)
    {
        this.score = score;
        scoreText.text = (score / 1000).ToString() + "k";

        float scoreDependent = (float)score / topScore;

        if (scoreDependent < 0.2f)
        {
            scoreText.transform.position += new Vector3(-4, 0, 0);
            scoreText.alignment = TextAnchor.MiddleRight;
        }
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
