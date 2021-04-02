using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Piece : MonoBehaviour
{
    public Position Position;

    public UnityEvent onMoved;

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

    public void MoveToPosition(Position position)
    {
        if (position == Position)
        {
            GetComponent<DragAndDrop>().ResetPosition();
            return;
        }
        
        if (CanMoveTo(position))
        {
            UpdatePosition(position);
        }
        else if (CanAttackAt(position))
        {
            Piece other = GameController.Instance.GetPieceAtPosition(position);
            if (other == null) // En passant case
            {
                int direction = isWhite == GameController.Instance.isWhiteDown ? -1 : 1;
                other = GameController.Instance.GetPieceAtPosition(position.GetPositionFromHere(0, direction));
            }
            GameController.Instance.pieces.Remove(other);
            Destroy(other.gameObject);
            UpdatePosition(position);
        }
        else
            GetComponent<DragAndDrop>().ResetPosition();
    }

    private void UpdatePosition(Position position)
    {
        if (HasPawnMovedTwoSquares(position, out int direction))
        {
            Position enPassantPosition = Position.GetPositionFromHere(0, direction);
            GameController.Instance.enPassant = GameController.Instance.squares[enPassantPosition.GetNotation()];
        }
        else
        {
            GameController.Instance.enPassant = null;
        }
        
        Square square = GameController.Instance.squares[position.GetNotation()];
        GetComponent<RectTransform>().anchoredPosition = square.GetComponent<RectTransform>().anchoredPosition;
        Position = position;
        onMoved.Invoke();
        
        GetComponent<Movable>().ResetMoves();
        GameController.Instance.isWhiteTurn = !GameController.Instance.isWhiteTurn;
    }

    private bool HasPawnMovedTwoSquares(Position position, out int direction)
    {
        direction = isWhite == GameController.Instance.isWhiteDown ? 1 : -1;
        return type == PieceType.Pawn && position.Equals(Position.GetPositionFromHere(0, 2*direction));
    }

    private bool CanMoveTo(Position position)
    {
        foreach (Position legalMove in GetComponent<Movable>().LegalMoves)
        {
            if (position.Equals(legalMove))
                return true;
        }

        return false;
    }

    private bool CanAttackAt(Position position)
    {
        foreach (Position attackMove in GetComponent<Movable>().AttackMoves)
        {
            if (position.Equals(attackMove))
                return true;
        }

        return false;
    }

    public override string ToString()
    {
        string color = isWhite ? "White" : "Black";
        return color + " " + type.ToString() + " " + Position;
    }
}
