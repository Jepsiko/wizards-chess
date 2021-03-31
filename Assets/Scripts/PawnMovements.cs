using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovements : MonoBehaviour, IMovable
{
    public List<string> GetPossibleMoves()
    {
        List<string> possibleMoves = new List<string>();
        bool isWhite = GetComponent<Piece>().isWhite;

        string position = GetComponent<Piece>().position;
        int[] coord = BoardNotation.CoordinateFromSquareName(position, true);
        int file = coord[0];
        int rank = coord[1];

        if (isWhite)
        {
            possibleMoves.Add(BoardNotation.SquareNameFromCoordinate(file, rank+1, true));
            if (rank == 1)
                possibleMoves.Add(BoardNotation.SquareNameFromCoordinate(file, rank+2, true));
        }
        else
        {
            possibleMoves.Add(BoardNotation.SquareNameFromCoordinate(file, rank-1, true));
            if (rank == 6)
                possibleMoves.Add(BoardNotation.SquareNameFromCoordinate(file, rank-2, true));
        }

        possibleMoves.ForEach(print);
        return possibleMoves;
    }
}
