using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movable : MonoBehaviour
{
    public List<Position> PossibleMoves;
    public List<Position> LegalMoves;
    public List<Position> AttackMoves;

    public void ResetMoves()
    {
        PossibleMoves = new List<Position>();
        LegalMoves = new List<Position>();
        AttackMoves = new List<Position>();
    }

    public abstract void GeneratePossibleMoves();

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
