using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawBoard : MonoBehaviour
{
    public int size;

    private void Awake()
    {
        for (int file = 0; file < 8; file++)
        {
            for (int rank = 0; rank < 8; rank++)
            {
                GameObject square = CreateSquare(file, rank);
                square.name = BoardNotation.SquareNameFromCoordinate(file, rank);
                square.AddComponent<Square>().position = square.name;
                GameController.Instance.squares.Add(square.name, square.GetComponent<Square>());
            }
        }
    }

    private void Update()
    {
        foreach (DictionaryEntry squareEntry in GameController.Instance.squares)
        {
            Square square = (Square) squareEntry.Value;
            if (square.isLegal)
                square.ChangeColor(GetLegalSquareColor(square.position));
            else
                square.ChangeColor(GetSquareColor(square.position));
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

        return square;
    }

    private Vector3 GetPosition(int file, int rank)
    {
        return new Vector3((-3.50f + file)*size, (-3.50f + rank)*size);
    }

    private Color GetSquareColor(string position)
    {
        int[] coord = BoardNotation.CoordinateFromSquareName(position);
        int file = coord[0];
        int rank = coord[1];
        return GetSquareColor(file, rank);
    }

    private Color GetSquareColor(int file, int rank)
    {
        return (file + rank) % 2 == 0 ? GameSettings.Instance.blackColor : GameSettings.Instance.whiteColor;
    }

    private Color GetLegalSquareColor(string position)
    {
        int[] coord = BoardNotation.CoordinateFromSquareName(position);
        int file = coord[0];
        int rank = coord[1];
        return GetLegalSquareColor(file, rank);
    }

    private Color GetLegalSquareColor(int file, int rank)
    {
        return (file + rank) % 2 == 0 ? GameSettings.Instance.blackLegalMoveColor : GameSettings.Instance.whiteLegalMoveColor;
    }
}