using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovements : Movable
{
    public override void GeneratePossibleMoves()
    {
        PossibleMoves = new List<Position>();
        bool isWhite = GetComponent<Piece>().isWhite;

        Position position = GetComponent<Piece>().Position;
        bool isWhiteDown = GameController.Instance.isWhiteDown;
        
        int file = position.GetFile();
        int rank = position.GetRank();

        if (isWhite == isWhiteDown)
        {
            PossibleMoves.Add(Position.GetPositionAt(file, rank+1));
            if (rank == 1)
                PossibleMoves.Add(Position.GetPositionAt(file, rank+2));
        }
        else
        {
            PossibleMoves.Add(Position.GetPositionAt(file, rank-1));
            if (rank == 6)
                PossibleMoves.Add(Position.GetPositionAt(file, rank-2));
        }
    }
}
