using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Position Position;

    public enum PieceType
    {
        Pawn,
        Queen,
        King,
        Knight,
        Bishop,
        Rook
    }

    public PieceType type;
    public bool isWhite;

    public void MoveToPosition(Position newPosition)
    {
        if (newPosition == Position)
        {
            GetComponent<DragAndDrop>().ResetPosition();
            return;
        }
        
        if (CanMoveTo(newPosition))
        {
            Square square = GameController.Instance.squares[newPosition.GetNotation()];
            Debug.Log("Square " + square.name + ", " + square.Position.GetNotation(), square);
            GetComponent<RectTransform>().anchoredPosition = square.GetComponent<RectTransform>().anchoredPosition;
            Position = newPosition;
        }
        else
            GetComponent<DragAndDrop>().ResetPosition();
    }

    private bool CanMoveTo(Position newPosition)
    {
        foreach (Position legalMove in GetComponent<Movable>().LegalMoves)
        {
            if (newPosition.Equals(legalMove))
                return true;
        }

        return false;
    }
}
