using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movable : MonoBehaviour
{
    public List<Position> PossibleMoves;
    public List<Position> LegalMoves;
    public List<Position> AttackMoves;

    private void Start()
    {
        ResetMoves();
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
        Piece piece = GetComponent<Piece>();
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
        Position position = GetComponent<Piece>().Position;
        Position nextPosition = position.GetPositionFromHere(fileOffset, rankOffset);
        while (nextPosition != null && !GameController.Instance.IsOccupied(nextPosition))
        {
            PossibleMoves.Add(nextPosition);
            nextPosition = nextPosition.GetPositionFromHere(fileOffset, rankOffset);
        }
        
        if (GameController.Instance.IsOccupied(nextPosition))
            PossibleMoves.Add(nextPosition);
    }
}
