using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public string position;

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

    public void MoveToPosition(string newPosition)
    {
        List<string> legalMoves = GetComponent<IMovable>().GetLegalMoves();
        if (newPosition != position && legalMoves.Contains(newPosition))
        {
            Square square = (Square) GameController.Instance.squares[newPosition];
            GetComponent<RectTransform>().anchoredPosition = square.GetComponent<RectTransform>().anchoredPosition;
            position = newPosition;
        }
        else
            GetComponent<DragAndDrop>().ResetPosition();
    }
}
