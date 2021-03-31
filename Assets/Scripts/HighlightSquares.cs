using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightSquares : MonoBehaviour
{
    private List<Square> highlightedSquares;

    private void Start()
    {
        highlightedSquares = new List<Square>();
    }

    public void HighlightLegalSquares()
    {
        foreach (string squarePosition in GetComponent<IMovable>().GetLegalMoves())
        {
            Square square = (Square) GameController.Instance.squares[squarePosition];
            square.isLegal = true;
            highlightedSquares.Add(square);
        }
    }

    public void UnhighlightLegalSquares()
    {
        for (int i = highlightedSquares.Count - 1; i >= 0; i--)
        {
            highlightedSquares[i].isLegal = false;
            highlightedSquares.RemoveAt(i);
        }
    }
}
