using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseHoverInfo : MonoBehaviour
{
    public string text;

    public void OnMouseEnter()
    {
        MouseTextBox.SetText(text);
        MouseTextBox.Show();
    }
    public void OnMouseExit()
    {
        MouseTextBox.Hide();
    }
}
