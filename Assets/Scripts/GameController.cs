﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    
    public bool isWhiteDown;
    public bool isWhiteTurn = true;

    public UnityEvent onMovesGenerated;
    public UnityEvent onAnyPieceMoved;
    
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

    public void NextTurn()
    {
        isWhiteTurn = !isWhiteTurn;
        onAnyPieceMoved.Invoke();
    }
}
