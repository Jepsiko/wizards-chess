using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawBoard : MonoBehaviour
{
    public Color whiteColor;
    public Color blackColor;
    public int size;

    private Image[][] squares;

    private void Awake()
    {
        squares = new Image[8][];
        for (int file = 0; file < 8; file++)
        {
            squares[file] = new Image[8];
            for (int rank = 0; rank < 8; rank++)
            {
                GameObject square = CreateSquare(file, rank);
                square.name = BoardNotation.SquareNameFromCoordinate(file, rank, GameController.Instance.isWhiteDown);
                squares[file][rank] = square.GetComponent<Image>();
            }
        }
    }

    private void Update()
    {
        for (int file = 0; file < 8; file++)
        {
            for (int rank = 0; rank < 8; rank++)
            {
                squares[file][rank].color = GetSquareColor(file, rank);
            }
        }
    }

    private GameObject CreateSquare(int file, int rank)
    {
        GameObject square = new GameObject();
        
        RectTransform rectTransform = square.AddComponent<RectTransform>();
        rectTransform.SetParent(transform);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        rectTransform.anchoredPosition = GetPosition(file, rank);

        Image squareImage = square.AddComponent<Image>();
        squareImage.color = GetSquareColor(file, rank);

        square.AddComponent<PieceSlot>();
        return square;
    }

    private Vector3 GetPosition(int file, int rank)
    {
        return new Vector3((-3.50f + file)*size, (-3.50f + rank)*size);
    }

    private Color GetSquareColor(int file, int rank)
    {
        return (file + rank) % 2 == 0 ? blackColor : whiteColor;
    }
}