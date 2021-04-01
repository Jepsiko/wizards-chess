using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovements : MonoBehaviour, IMovable
{
    public List<string> GetPossibleMoves()
    {
        List<string> possibleMoves = new List<string>();

        string position = GetComponent<Piece>().position;

        for (int i = -2; i <= 2; i++)
            for (int j = -2; j <= 2; j++)
                if (i != 0 && j != 0 && i != j && i != -j)
                {
                    string nextPosition = BoardNotation.MoveFileAndRank(position, i, j);
                    if (nextPosition != null && !GameController.Instance.IsOccupied(nextPosition)) 
                        possibleMoves.Add(nextPosition);
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
