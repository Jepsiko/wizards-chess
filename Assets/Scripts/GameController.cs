using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    
    public bool isWhiteDown;
    public List<Piece> pieces = new List<Piece>();
    public Hashtable squares = new Hashtable();
    
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
}
