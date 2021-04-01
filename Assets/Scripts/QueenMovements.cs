using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMovements : MonoBehaviour, IMovable
{
    public List<string> GetPossibleMoves()
    {
        List<string> possibleMoves = new List<string>();

        string position = GetComponent<Piece>().position;

        AddAlongLine(possibleMoves, position, -1, 0);
        AddAlongLine(possibleMoves, position, 0, -1);
        AddAlongLine(possibleMoves, position, +1, 0);
        AddAlongLine(possibleMoves, position, 0, +1);

        AddAlongLine(possibleMoves, position, -1, -1);
        AddAlongLine(possibleMoves, position, -1, +1);
        AddAlongLine(possibleMoves, position, +1, -1);
        AddAlongLine(possibleMoves, position, +1, +1);

        return possibleMoves;
    }

    private void AddAlongLine(List<string> possibleMoves, string position, int fileOffset, int rankOffset)
    {
        string nextPosition = BoardNotation.MoveFileAndRank(position, fileOffset, rankOffset);
        while (nextPosition != null && !GameController.Instance.IsOccupied(nextPosition))
        {
            possibleMoves.Add(nextPosition);
            nextPosition = BoardNotation.MoveFileAndRank(nextPosition, fileOffset, rankOffset);
        }
    }

    public List<string> GetLegalMoves()
    {
        List<string> possibleMoves = GetPossibleMoves();
        List<string> legalMoves = new List<string>();

        foreach (string possibleMove in possibleMoves)
        {
            if (!GameController.Instance.IsOccupied(possibleMove))
                legalMoves.Add(possibleMove);
        }
        
        return legalMoves;
    }
}
