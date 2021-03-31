﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    List<string> GetPossibleMoves();
    List<string> GetLegalMoves();
}
