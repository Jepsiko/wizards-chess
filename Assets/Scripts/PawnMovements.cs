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
        bool isWhiteDown = GameController.Instance.isWhiteDown;
        
        int[] coord = BoardNotation.CoordinateFromSquareName(position, isWhiteDown);
        int file = coord[0];
        int rank = coord[1];

        if (isWhite == isWhiteDown)
        {
            possibleMoves.Add(BoardNotation.SquareNameFromCoordinate(file, rank+1, isWhiteDown));
            if (rank == 1)
                possibleMoves.Add(BoardNotation.SquareNameFromCoordinate(file, rank+2, isWhiteDown));
        }
        else
        {
            possibleMoves.Add(BoardNotation.SquareNameFromCoordinate(file, rank-1, isWhiteDown));
            if (rank == 6)
                possibleMoves.Add(BoardNotation.SquareNameFromCoordinate(file, rank-2, isWhiteDown));
        }

        return possibleMoves;
    }

    public List<string> GetLegalMoves()
    {
        List<string> legalMoves = GetPossibleMoves();
        
        
        
        return legalMoves;
    }
}
