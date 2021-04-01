using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;
    
    public Color whiteColor;
    public Color blackColor;
    public Color whiteLegalMoveColor;
    public Color blackLegalMoveColor;
    public Color whiteAttackMoveColor;
    public Color blackAttackMoveColor;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple instance of GameSettings, ignoring this one", this);
        }
    }
}
