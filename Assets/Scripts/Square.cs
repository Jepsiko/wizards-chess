using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Square : MonoBehaviour
{
    public string position;
    public Color color;
    public bool isLegal = false;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void ChangeColor(Color newColor)
    {
        color = newColor;
        image.color = newColor;
    }
}
