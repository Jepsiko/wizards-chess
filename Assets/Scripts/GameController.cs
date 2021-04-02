﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    
    public bool isWhiteDown;

    [HideInInspector]
    public bool isWhiteTurn = true;

    [HideInInspector]
    public UnityEvent onMovesGenerated;
    
    [HideInInspector]
    public List<Piece> pieces = new List<Piece>();
    public Dictionary<string, Square> squares = new Dictionary<string, Square>();
    [HideInInspector]
    public Square enPassant;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple instance of GameController, ignoring this one", this);
        }
    }
    
    public bool IsOccupied(Position position)
    {
        return GetPieceAtPosition(position) != null;
    }

    public Piece GetPieceAtPosition(Position position)
    {
        foreach (Piece piece in pieces)
            if (piece.Position.Equals(position))
                return piece;

        return null;
    }
    
    public void GenerateMoves(Piece piece)
    {
        Movable movable = piece.GetComponent<Movable>();
        movable.ResetMoves();
        
        if (piece.isWhite != isWhiteTurn)
            return;
        
        movable.GeneratePossibleMoves();

        foreach (Position possibleMove in movable.PossibleMoves)
        {
            Piece other = GetPieceAtPosition(possibleMove);
            if (other == null)
                movable.LegalMoves.Add(possibleMove);
            else if (piece.isWhite != other.isWhite)
                movable.AttackMoves.Add(possibleMove);
        }

        onMovesGenerated.Invoke();
    }
}
