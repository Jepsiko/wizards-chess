using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovements : Movable
{

    private void GenerateAttackMoves()
    {
        Position position = GetComponent<Piece>().Position;

        bool directionUp = GetComponent<Piece>().isWhite == GameController.Instance.isWhiteDown;
        int direction = directionUp ? 1 : -1;

        Position left = position.GetPositionFromHere(-1, +1 * direction);
        Position right = position.GetPositionFromHere(+1, +1 * direction);

        if (IsEnemyThere(left))
            AttackMoves.Add(left);
        if (IsEnemyThere(right))
            AttackMoves.Add(right);
    }

    private bool IsEnemyThere(Position position)
    {
        Piece piece = GameController.Instance.GetPieceAtPosition(position);
        print(piece);
        return piece != null && piece.isWhite != GetComponent<Piece>().isWhite;
    }

    public override void GeneratePossibleMoves()
    {
        PossibleMoves = new List<Position>();

        Position position = GetComponent<Piece>().Position;
        int rank = position.GetRank();

        bool directionUp = GetComponent<Piece>().isWhite == GameController.Instance.isWhiteDown;
        int direction = directionUp ? 1 : -1;
        int startingRank = directionUp ? 1 : 6;
        
        PossibleMoves.Add(position.GetPositionFromHere(0, +1*direction));
        if (rank == startingRank)
            PossibleMoves.Add(position.GetPositionFromHere(0, +2*direction));

        GenerateAttackMoves();
    }
}
