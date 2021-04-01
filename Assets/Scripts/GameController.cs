using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    
    public bool isWhiteDown;
    
    [HideInInspector]
    public List<Piece> pieces = new List<Piece>();
    public Dictionary<string, Square> squares = new Dictionary<string, Square>();
    
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
        movable.GeneratePossibleMoves();
        movable.LegalMoves = new List<Position>();
        movable.AttackMoves = new List<Position>();

        foreach (Position possibleMove in movable.PossibleMoves)
        {
            Piece other = GetPieceAtPosition(possibleMove);
            if (other == null)
                movable.LegalMoves.Add(possibleMove);
            else if (piece.isWhite != other.isWhite)
                movable.AttackMoves.Add(possibleMove);
        }
    }
}
