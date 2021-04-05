using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movable : MonoBehaviour
{
    public List<Position> PossibleMoves;
    public List<Position> LegalMoves;
    public List<Position> AttackMoves;

    protected Piece piece;

    private void Start()
    {
        ResetMoves();
        piece = GetComponent<Piece>();
    }

    public void ResetMoves()
    {
        PossibleMoves = new List<Position>();
        LegalMoves = new List<Position>();
        AttackMoves = new List<Position>();
    }

    public abstract void GeneratePossibleMoves();
    
    public void GenerateMoves()
    {
        ResetMoves();
        
        if (piece.isWhite != GameController.Instance.isWhiteTurn)
            return;
        
        GeneratePossibleMoves();

        foreach (Position possibleMove in PossibleMoves)
        {
            Piece other = GameController.Instance.GetPieceAtPosition(possibleMove);
            if (other == null)
                LegalMoves.Add(possibleMove);
            else if (piece.isWhite != other.isWhite)
                AttackMoves.Add(possibleMove);
        }

        GameController.Instance.onMovesGenerated.Invoke();
    }

    protected void AddAlongLine(int fileOffset, int rankOffset)
    {
        Position nextPosition = piece.Position.GetPositionFromHere(fileOffset, rankOffset);
        while (nextPosition != null && !GameController.Instance.IsOccupied(nextPosition))
        {
            PossibleMoves.Add(nextPosition);
            nextPosition = nextPosition.GetPositionFromHere(fileOffset, rankOffset);
        }
        
        if (GameController.Instance.IsOccupied(nextPosition))
            PossibleMoves.Add(nextPosition);
    }
}
