using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookMovements : MonoBehaviour, IMovable
{
    public List<string> GetPossibleMoves()
    {
        List<string> possibleMoves = new List<string>();

        string position = GetComponent<Piece>().position;
        
        int[] coord = BoardNotation.CoordinateFromSquareName(position);

        for (int file = coord[0]+1; file < 8; file++)
        {
            string nextPosition = BoardNotation.SquareNameFromCoordinate(file, coord[1]);
            if (GameController.Instance.IsOccupied(nextPosition))
                break;
            possibleMoves.Add(nextPosition);
        }

        for (int file = coord[0]-1; file >= 0; file--)
        {
            string nextPosition = BoardNotation.SquareNameFromCoordinate(file, coord[1]);
            if (GameController.Instance.IsOccupied(nextPosition))
                break;
            possibleMoves.Add(nextPosition);
        }

        for (int rank = coord[1]+1; rank < 8; rank++)
        {
            string nextPosition = BoardNotation.SquareNameFromCoordinate(coord[0], rank);
            if (GameController.Instance.IsOccupied(nextPosition))
                break;
            possibleMoves.Add(nextPosition);
        }

        for (int rank = coord[1]-1; rank >= 0; rank--)
        {
            string nextPosition = BoardNotation.SquareNameFromCoordinate(coord[0], rank);
            if (GameController.Instance.IsOccupied(nextPosition))
                break;
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
