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
        
        int[] coord = BoardNotation.CoordinateFromSquareName(position);
        int file = coord[0];
        int rank = coord[1];

        if (isWhite == isWhiteDown)
        {
            possibleMoves.Add(BoardNotation.SquareNameFromCoordinate(file, rank+1));
            if (rank == 1)
                possibleMoves.Add(BoardNotation.SquareNameFromCoordinate(file, rank+2));
        }
        else
        {
            possibleMoves.Add(BoardNotation.SquareNameFromCoordinate(file, rank-1));
            if (rank == 6)
                possibleMoves.Add(BoardNotation.SquareNameFromCoordinate(file, rank-2));
        }

        return possibleMoves;
    }

    public List<string> GetLegalMoves()
    {
        List<string> possibleMoves = GetPossibleMoves();
        List<string> legalMoves = new List<string>();

        foreach (string possibleMove in possibleMoves)
        {
            if (!IsOccupied(possibleMove))
                legalMoves.Add(possibleMove);
        }
        
        return legalMoves;
    }

    private bool IsOccupied(string position)
    {
        bool isOccupied = false;

        foreach (Piece piece in GameController.Instance.pieces)
        {
            if (piece.position == position)
            {
                isOccupied = true;
                break;
            }
        }

        return isOccupied;
    }
}
