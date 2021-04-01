using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMovements : MonoBehaviour, IMovable
{
    public List<string> GetPossibleMoves()
    {
        List<string> possibleMoves = new List<string>();

        string position = GetComponent<Piece>().position;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i != 0 || j != 0)
                {
                    string nextPosition = BoardNotation.MoveFileAndRank(position, i, j);
                    if (nextPosition != null && !GameController.Instance.IsOccupied(nextPosition))
                    {
                        possibleMoves.Add(nextPosition);
                    }
                }
            }
        }

        return possibleMoves;
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
