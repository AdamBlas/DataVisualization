using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseTextBox : MonoBehaviour
{
    static MouseTextBox @this;
    static Transform t;
    Text text;



    // Start is called before the first frame update
    void Start()
    {
        @this = this;
        text = GetComponentInChildren<Text>();
        t = transform;

        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        t.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(1, 0, 1);
    }

    public static void SetText(string text)
    {
        @this.text.text = text;
    }
    public static void Show()
    {
        @this.gameObject.SetActive(true);
    }
    public static void Hide()
    {
        @this.gameObject.SetActive(false);
    }
}
