using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovements : Movable
{

    private void GenerateAttackMoves()
    {
        Position position = GetComponent<Piece>().Position;

        bool isDown = GetComponent<Piece>().isWhite == GameController.Instance.isWhiteDown;
        int direction = isDown ? 1 : -1;

        Position left = position.GetPositionFromHere(-1, +1 * direction);
        Position right = position.GetPositionFromHere(+1, +1 * direction);

        if (piece.IsEnemyThere(left) || EnPassantPossible(left))
            AttackMoves.Add(left);
        if (piece.IsEnemyThere(right) || EnPassantPossible(right))
            AttackMoves.Add(right);
    }

    private bool EnPassantPossible(Position position)
    {
        Square enPassant = GameController.Instance.enPassant;
        return enPassant != null && enPassant.Position.Equals(position);
    }

    public override void GeneratePossibleMoves()
    {
        int rank = piece.Position.GetRank();

        bool upward = GetComponent<Piece>().isWhite == GameController.Instance.isWhiteDown;
        int direction = upward ? 1 : -1;
        int startingRank = upward ? 1 : 6;

        Position nextPosition = piece.Position.GetPositionFromHere(0, +1*direction);
        if (GameController.Instance.GetPieceAtPosition(nextPosition) == null)
            PossibleMoves.Add(nextPosition);
        
        nextPosition = piece.Position.GetPositionFromHere(0, +2*direction);
        if (rank == startingRank)
            if (GameController.Instance.GetPieceAtPosition(nextPosition) == null)
                PossibleMoves.Add(nextPosition);

        GenerateAttackMoves();
    }
}
