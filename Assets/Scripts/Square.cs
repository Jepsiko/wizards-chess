﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Square : MonoBehaviour
{
    public Position Position;
    public Color color;
    public bool isLegal;
    public bool isAttacked;

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
