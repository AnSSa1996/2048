using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public Tile[,] Tiles;
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
        Tiles = new Tile[Width, Height];
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                Tiles[x, y] = new Tile(x, y);
            }
        }
    }

    public void Destroy()
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                Tiles[x, y].Clear();
            }
        }

        Tiles = null;
    }

    public void MoveRight()
    {
        for (var x = Width - 1; x >= 0; x--)
        {
            for (var y = 0; y < Height; y++)
            {
                if (Tiles[x, y].IsEmpty)
                {
                    continue;
                }

                var nextX = x + 1;
                while (nextX < Width)
                {
                    if (Tiles[nextX, y].IsEmpty)
                    {
                        Tiles[nextX, y].SetScore(Tiles[x, y].Score);
                        Tiles[x, y].Clear();
                        x = nextX;
                    }
                    else if (Tiles[nextX, y].Score == Tiles[x, y].Score)
                    {
                        Tiles[nextX, y].SetScore(Tiles[x, y].Score * 2);
                        Tiles[x, y].Clear();
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
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                if (Tiles[x, y].IsEmpty)
                {
                    continue;
                }

                var nextX = x - 1;
                while (nextX >= 0)
                {
                    if (Tiles[nextX, y].IsEmpty)
                    {
                        Tiles[nextX, y].SetScore(Tiles[x, y].Score);
                        Tiles[x, y].Clear();
                        x = nextX;
                    }
                    else if (Tiles[nextX, y].Score == Tiles[x, y].Score)
                    {
                        Tiles[nextX, y].SetScore(Tiles[x, y].Score * 2);
                        Tiles[x, y].Clear();
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
        for (var y = Height - 1; y >= 0; y--)
        {
            for (var x = 0; x < Width; x++)
            {
                if (Tiles[x, y].IsEmpty)
                {
                    continue;
                }

                var nextY = y + 1;
                while (nextY < Height)
                {
                    if (Tiles[x, nextY].IsEmpty)
                    {
                        Tiles[x, nextY].SetScore(Tiles[x, y].Score);
                        Tiles[x, y].Clear();
                        y = nextY;
                    }
                    else if (Tiles[x, nextY].Score == Tiles[x, y].Score)
                    {
                        Tiles[x, nextY].SetScore(Tiles[x, y].Score * 2);
                        Tiles[x, y].Clear();
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
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (Tiles[x, y].IsEmpty)
                {
                    continue;
                }

                var nextY = y - 1;
                while (nextY >= 0)
                {
                    if (Tiles[x, nextY].IsEmpty)
                    {
                        Tiles[x, nextY].SetScore(Tiles[x, y].Score);
                        Tiles[x, y].Clear();
                        y = nextY;
                    }
                    else if (Tiles[x, nextY].Score == Tiles[x, y].Score)
                    {
                        Tiles[x, nextY].SetScore(Tiles[x, y].Score * 2);
                        Tiles[x, y].Clear();
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