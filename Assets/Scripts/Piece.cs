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
            Destroy(other.gameObject);
            UpdatePosition(position);
        }
        else
            GetComponent<DragAndDrop>().ResetPosition();
    }

    private void UpdatePosition(Position position)
    {
        Square square = GameController.Instance.squares[position.GetNotation()];
        GetComponent<RectTransform>().anchoredPosition = square.GetComponent<RectTransform>().anchoredPosition;
        Position = position;
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
}
