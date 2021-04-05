using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkable : MonoBehaviour
{
    public bool isChecked;
    
    private Piece piece;
    private UnityEvent onChecked;

    private void Start()
    {
        piece = GetComponent<Piece>();
        GameController.Instance.onAnyPieceMoved.AddListener(CheckForChecking);
    }

    private void CheckForChecking()
    {
        isChecked = IsCheckedByPawn(piece.Position) || 
                    IsCheckedByKnight(piece.Position) || 
                    IsCheckedByRookOrQueen(piece.Position) || 
                    IsCheckedByBishopOrQueen(piece.Position) ||
                    IsCheckedByKing(piece.Position);
        
        // if (isChecked) onChecked.Invoke();
    }

    private bool IsCheckedByKing(Position position)
    {
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (i != 0 || j != 0)
                {
                    Position nextPosition = position.GetPositionFromHere(i, j);
                    if (piece.IsEnemyKingThere(nextPosition))
                        return true;
                }

        return false;
    }

    private bool IsCheckedByKnight(Position position)
    {
        for (int i = -2; i <= 2; i++)
            for (int j = -2; j <= 2; j++)
                if (i != 0 && j != 0 && i != j && i != -j)
                {
                    Position knightPosition = position.GetPositionFromHere(i, j);
                    if (piece.IsEnemyKnightThere(knightPosition))
                        return true;
                }
        
        return false;
    }

    private bool IsCheckedByPawn(Position position)
    {
        bool isDown = piece.isWhite == GameController.Instance.isWhiteDown;
        int direction = isDown ? 1 : -1;

        Position left = position.GetPositionFromHere(-1, +1 * direction);
        Position right = position.GetPositionFromHere(+1, +1 * direction);

        return piece.IsEnemyPawnThere(left) || piece.IsEnemyPawnThere(right);
    }

    private bool IsCheckedByRookOrQueen(Position position)
    {
        return CheckAlongLine(position, Piece.PieceType.Rook, -1, 0) || 
               CheckAlongLine(position, Piece.PieceType.Rook, 0, -1) || 
               CheckAlongLine(position, Piece.PieceType.Rook, +1, 0) || 
               CheckAlongLine(position, Piece.PieceType.Rook, 0, +1);
    }

    private bool IsCheckedByBishopOrQueen(Position position)
    {
        return CheckAlongLine(position, Piece.PieceType.Bishop, -1, -1) || 
               CheckAlongLine(position, Piece.PieceType.Bishop, -1, +1) || 
               CheckAlongLine(position, Piece.PieceType.Bishop, +1, -1) || 
               CheckAlongLine(position, Piece.PieceType.Bishop, +1, +1);
    }

    private bool CheckAlongLine(Position position, Piece.PieceType type, int fileOffset, int rankOffset)
    {
        Position nextPosition = position.GetPositionFromHere(fileOffset, rankOffset);
        while (nextPosition != null && !GameController.Instance.IsOccupied(nextPosition))
        {
            nextPosition = nextPosition.GetPositionFromHere(fileOffset, rankOffset);
        }

        return piece.IsEnemyOfTypeHere(type, nextPosition) || 
               piece.IsEnemyOfTypeHere(Piece.PieceType.Queen, nextPosition);
    }
}
