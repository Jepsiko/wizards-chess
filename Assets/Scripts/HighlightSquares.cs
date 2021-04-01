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
        GetComponent<Movable>().LegalMoves.ForEach(print);
        foreach (Position squarePosition in GetComponent<Movable>().LegalMoves)
        {
            Square square = GameController.Instance.squares[squarePosition.GetNotation()];
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
