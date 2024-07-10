using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public Tile[,] TileArray;
    public GameObject[,] TileObjectArray;
    public int Width;
    public int Height;

    public Board(int width, int height)
    {
        Width = width;
        Height = height;
        Init();
    }

    public void Init()
    {
        TileArray = new Tile[Width, Height];
        TileObjectArray = new GameObject[Width, Height];
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                TileArray[x, y] = new Tile(x, y);
            }
        }
    }

    public void Destroy()
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                TileArray[x, y].Clear();
            }
        }

        TileArray = null;
    }
    
    public void CreateRandomTile()
    {
        
    }

    public void MoveRight()
    {
        var moveTiles = new List<MoveTile>();
        for (var x = Width - 1; x >= 0; x--)
        {
            for (var y = 0; y < Height; y++)
            {
                if (TileArray[x, y].IsEmpty)
                {
                    continue;
                }
                
                var nextX = x + 1;
                while (nextX < Width)
                {
                    if (TileArray[nextX, y].IsEmpty)
                    {
                        TileArray[nextX, y].SetScore(TileArray[x, y].Score);
                        TileArray[x, y].Clear();
                        x = nextX;
                        moveTiles.Add(new MoveTile(TileArray[nextX, y], x, y, nextX, y, false));
                    }
                    else if (TileArray[nextX, y].Score == TileArray[x, y].Score)
                    {
                        TileArray[nextX, y].SetScore(TileArray[x, y].Score * 2);
                        TileArray[x, y].Clear();
                        moveTiles.Add(new MoveTile(TileArray[nextX, y], x, y, nextX, y, true));
                    }
                    else
                    {
                        break;
                    }

                    nextX++;
                }
            }
        }
    }

    public void MoveLeft()
    {
        var moveTiles = new List<MoveTile>();
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                if (TileArray[x, y].IsEmpty)
                {
                    continue;
                }

                var nextX = x - 1;
                while (nextX >= 0)
                {
                    if (TileArray[nextX, y].IsEmpty)
                    {
                        TileArray[nextX, y].SetScore(TileArray[x, y].Score);
                        TileArray[x, y].Clear();
                        x = nextX;
                        moveTiles.Add(new MoveTile(TileArray[nextX, y], x, y, nextX, y, false));
                    }
                    else if (TileArray[nextX, y].Score == TileArray[x, y].Score)
                    {
                        TileArray[nextX, y].SetScore(TileArray[x, y].Score * 2);
                        TileArray[x, y].Clear();
                        moveTiles.Add(new MoveTile(TileArray[nextX, y], x, y, nextX, y, true));
                    }
                    else
                    {
                        break;
                    }

                    nextX--;
                }
            }
        }
    }

    public void MoveUp()
    {
        var moveTiles = new List<MoveTile>();
        for (var y = Height - 1; y >= 0; y--)
        {
            for (var x = 0; x < Width; x++)
            {
                if (TileArray[x, y].IsEmpty)
                {
                    continue;
                }

                var nextY = y + 1;
                while (nextY < Height)
                {
                    if (TileArray[x, nextY].IsEmpty)
                    {
                        TileArray[x, nextY].SetScore(TileArray[x, y].Score);
                        TileArray[x, y].Clear();
                        y = nextY;
                        moveTiles.Add(new MoveTile(TileArray[x, nextY], x, y, x, nextY, false));
                    }
                    else if (TileArray[x, nextY].Score == TileArray[x, y].Score)
                    {
                        TileArray[x, nextY].SetScore(TileArray[x, y].Score * 2);
                        TileArray[x, y].Clear();
                        moveTiles.Add(new MoveTile(TileArray[x, nextY], x, y, x, nextY, true));
                    }
                    else
                    {
                        break;
                    }

                    nextY++;
                }
            }
        }
    }

    public void MoveDown()
    {
        var moveTiles = new List<MoveTile>();
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (TileArray[x, y].IsEmpty)
                {
                    continue;
                }

                var nextY = y - 1;
                while (nextY >= 0)
                {
                    if (TileArray[x, nextY].IsEmpty)
                    {
                        TileArray[x, nextY].SetScore(TileArray[x, y].Score);
                        TileArray[x, y].Clear();
                        y = nextY;
                        moveTiles.Add(new MoveTile(TileArray[x, nextY], x, y, x, nextY, false));
                    }
                    else if (TileArray[x, nextY].Score == TileArray[x, y].Score)
                    {
                        TileArray[x, nextY].SetScore(TileArray[x, y].Score * 2);
                        TileArray[x, y].Clear();
                        moveTiles.Add(new MoveTile(TileArray[x, nextY], x, y, x, nextY, true));
                    }
                    else
                    {
                        break;
                    }

                    nextY--;
                }
            }
        }
    }
}