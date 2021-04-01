using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovements : Movable
{
    public override void GeneratePossibleMoves()
    {
        PossibleMoves = new List<Position>();

        Position position = GetComponent<Piece>().Position;

        for (int i = -2; i <= 2; i++)
            for (int j = -2; j <= 2; j++)
                if (i != 0 && j != 0 && i != j && i != -j)
                {
                    Position nextPosition = position.GetPositionFromHere(i, j);
                    if (nextPosition != null) 
                        PossibleMoves.Add(nextPosition);
                }
    }
}
