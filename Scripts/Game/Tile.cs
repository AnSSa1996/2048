using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Tile
{
    public int X;
    public int Y;
    public int Score;
    public bool IsEmpty;

    public Tile(int x, int y)
    {
        X = x;
        Y = y;
        Score = 0;
        IsEmpty = true;
    }
    
    public void SetScore(int score)
    {
        Score = score;
        IsEmpty = false;
    }
    
    public void Clear()
    {
        Score = 0;
        IsEmpty = true;
    }
}
