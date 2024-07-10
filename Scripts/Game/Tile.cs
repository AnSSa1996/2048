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

public interface TileAction
{
    void OnAction();
}

public class MoveTile : TileAction
{
    public Tile Tile;
    public int FromX;
    public int FromY;
    public int ToX;
    public int ToY;
    public bool IsMerged;

    public MoveTile(Tile tile, int fromX, int fromY, int toX, int toY, bool isMerged)
    {
        Tile = tile;
        FromX = fromX;
        FromY = fromY;
        ToX = toX;
        ToY = toY;
        IsMerged = isMerged;
    }

    public void OnAction()
    {
        var tile = GameManager.Instance.GameLogic.Board.TileObjectArray[FromX, FromY];
        if (tile == null) return;
        GameManager.Instance.GameLogic.Board.TileObjectArray[FromX, FromY] = null;
    }
}

public class CrateTile : TileAction
{
    public Tile Tile;

    public CrateTile(Tile tile)
    {
        Tile = tile;
    }

    public void OnAction()
    {
        
    }
}
