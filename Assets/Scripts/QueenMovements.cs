using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMovements : Movable
{
    public override void GeneratePossibleMoves()
    {
        PossibleMoves = new List<Position>();
        
        AddAlongLine(-1, 0);
        AddAlongLine(0, -1);
        AddAlongLine(+1, 0);
        AddAlongLine(0, +1);

        AddAlongLine(-1, -1);
        AddAlongLine(-1, +1);
        AddAlongLine(+1, -1);
        AddAlongLine(+1, +1);
    }
}
