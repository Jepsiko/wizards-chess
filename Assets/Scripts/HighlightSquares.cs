using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightSquares : MonoBehaviour
{
    private List<Square> highlightedLegalSquares;
    private List<Square> highlightedAttackSquares;

    private void Start()
    {
        highlightedLegalSquares = new List<Square>();
        highlightedAttackSquares = new List<Square>();
    }

    public void HighlightAllSquares()
    {
        HighlightLegalSquares();
        HighlightAttackSquares();
    }

    public void UnhighlightAllSquares()
    {
        UnhighlightLegalSquares();
        UnhighlightAttackSquares();
    }

    public void HighlightLegalSquares()
    {
        foreach (Position squarePosition in GetComponent<Movable>().LegalMoves)
        {
            Square square = GameController.Instance.squares[squarePosition.GetNotation()];
            square.isLegal = true;
            highlightedLegalSquares.Add(square);
        }
    }

    public void UnhighlightLegalSquares()
    {
        for (int i = highlightedLegalSquares.Count - 1; i >= 0; i--)
        {
            highlightedLegalSquares[i].isLegal = false;
            highlightedLegalSquares.RemoveAt(i);
        }
    }

    public void HighlightAttackSquares()
    {
        foreach (Position squarePosition in GetComponent<Movable>().AttackMoves)
        {
            Square square = GameController.Instance.squares[squarePosition.GetNotation()];
            square.isAttacked = true;
            highlightedAttackSquares.Add(square);
        }
    }

    public void UnhighlightAttackSquares()
    {
        for (int i = highlightedAttackSquares.Count - 1; i >= 0; i--)
        {
            highlightedAttackSquares[i].isAttacked = false;
            highlightedAttackSquares.RemoveAt(i);
        }
    }
}
