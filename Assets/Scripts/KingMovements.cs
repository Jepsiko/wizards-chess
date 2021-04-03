using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMovements : Movable
{
    public override void GeneratePossibleMoves()
    {
        Position position = GetComponent<Piece>().Position;

        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (i != 0 || j != 0)
                {
                    Position nextPosition = position.GetPositionFromHere(i, j);
                    if (nextPosition != null)
                        PossibleMoves.Add(nextPosition);
                }
    }
}
