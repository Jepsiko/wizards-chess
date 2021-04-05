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
        isChecked = IsCheckedByPawn() || IsCheckedByKnight() || IsCheckedByRookOrQueen() || IsCheckedByBishopOrQueen();
        if (isChecked) onChecked.Invoke();
    }

    private bool IsCheckedByKnight()
    {
        for (int i = -2; i <= 2; i++)
            for (int j = -2; j <= 2; j++)
                if (i != 0 && j != 0 && i != j && i != -j)
                {
                    Position knightPosition = piece.Position.GetPositionFromHere(i, j);
                    if (piece.IsEnemyKnightThere(knightPosition))
                        return true;
                }
        
        return false;
    }

    private bool IsCheckedByPawn()
    {
        bool isDown = piece.isWhite == GameController.Instance.isWhiteDown;
        int direction = isDown ? 1 : -1;

        Position left = piece.Position.GetPositionFromHere(-1, +1 * direction);
        Position right = piece.Position.GetPositionFromHere(+1, +1 * direction);

        return piece.IsEnemyPawnThere(left) || piece.IsEnemyPawnThere(right);
    }

    private bool IsCheckedByRookOrQueen()
    {
        return CheckAlongLine(Piece.PieceType.Rook, -1, 0) || 
               CheckAlongLine(Piece.PieceType.Rook, 0, -1) || 
               CheckAlongLine(Piece.PieceType.Rook, +1, 0) || 
               CheckAlongLine(Piece.PieceType.Rook, 0, +1);
    }

    private bool IsCheckedByBishopOrQueen()
    {
        return CheckAlongLine(Piece.PieceType.Bishop, -1, -1) || 
               CheckAlongLine(Piece.PieceType.Bishop, -1, +1) || 
               CheckAlongLine(Piece.PieceType.Bishop, +1, -1) || 
               CheckAlongLine(Piece.PieceType.Bishop, +1, +1);
    }

    private bool CheckAlongLine(Piece.PieceType type, int fileOffset, int rankOffset)
    {
        Position nextPosition = piece.Position.GetPositionFromHere(fileOffset, rankOffset);
        while (nextPosition != null && !GameController.Instance.IsOccupied(nextPosition))
        {
            nextPosition = nextPosition.GetPositionFromHere(fileOffset, rankOffset);
        }

        return piece.IsEnemyOfTypeHere(type, nextPosition) || 
               piece.IsEnemyOfTypeHere(Piece.PieceType.Queen, nextPosition);
    }
}
