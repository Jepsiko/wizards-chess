using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopMovements : Movable
{
    public override void GeneratePossibleMoves()
    {
        AddAlongLine(-1, -1);
        AddAlongLine(-1, +1);
        AddAlongLine(+1, -1);
        AddAlongLine(+1, +1);
    }
}
