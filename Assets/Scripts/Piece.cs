using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [HideInInspector]
    public String position;

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
}
