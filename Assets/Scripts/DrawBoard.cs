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
                Square square = CreateSquare(file, rank);
                GameController.Instance.squares.Add(square.Position.GetNotation(), square);
            }
        }
    }

    private void Update()
    {
        foreach (Square square in GameController.Instance.squares.Values)
        {
            if (square.isLegal)
                square.ChangeColor(GetLegalSquareColor(square.Position));
            else if (square.isAttacked)
                square.ChangeColor(GetAttackSquareColor(square.Position));
            else
                square.ChangeColor(GetSquareColor(square.Position));
        }
    }

    private Square CreateSquare(int file, int rank)
    {
        GameObject squareObject = new GameObject();
        
        RectTransform rectTransform = squareObject.AddComponent<RectTransform>();
        rectTransform.SetParent(transform);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        rectTransform.anchoredPosition = GetPosition(file, rank);

        squareObject.AddComponent<Image>();
        
        Square square = squareObject.AddComponent<Square>();
        square.Position = Position.GetPositionAt(file, rank);
        squareObject.name = square.Position.GetNotation();

        return square;
    }

    private Vector3 GetPosition(int file, int rank)
    {
        return new Vector3((-3.50f + file)*size, (-3.50f + rank)*size);
    }

    private Color GetSquareColor(Position position)
    {
        return GetSquareColor(position.GetFile(), position.GetRank());
    }

    private Color GetSquareColor(int file, int rank)
    {
        return (file + rank) % 2 == 0 ? GameSettings.Instance.blackColor : GameSettings.Instance.whiteColor;
    }

    private Color GetLegalSquareColor(Position position)
    {
        return GetLegalSquareColor(position.GetFile(), position.GetRank());
    }

    private Color GetLegalSquareColor(int file, int rank)
    {
        return (file + rank) % 2 == 0 ? GameSettings.Instance.blackLegalMoveColor : GameSettings.Instance.whiteLegalMoveColor;
    }

    private Color GetAttackSquareColor(Position position)
    {
        return GetAttackSquareColor(position.GetFile(), position.GetRank());
    }

    private Color GetAttackSquareColor(int file, int rank)
    {
        return (file + rank) % 2 == 0 ? GameSettings.Instance.blackAttackMoveColor : GameSettings.Instance.whiteAttackMoveColor;
    }
}