using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    private readonly int _length;
    
    public Tile[,] TileArray;
    public GameObject[,] TileGameObjectArray;
    private long Index { get; set; }

    public Board(int length)
    {
        _length = length;
    }
    
    public void Init()
    {
        Index = 0;
        TileArray = new Tile[_length, _length];
        TileGameObjectArray = new GameObject[_length, _length];
        for (var x = 0; x < _length; x++)
        {
            for (var y = 0; y < _length; y++)
            {
                TileArray[y, x] = new Tile(0, true);
            }
        }

        CreateRandomTile();
    }

    public void Destroy()
    {
        for (var x = 0; x < _length; x++)
        {
            for (var y = 0; y < _length; y++)
            {
                TileArray[x, y].Clear();
                TileGameObjectArray[x, y].ReleaseGameObject();
                TileGameObjectArray[x, y] = null;
            }
        }

        TileArray = null;
    }
    
    public void Move(Direction direction)
    {
        Index++;
        switch (direction)
        {
            case Direction.Right:
                MoveRight();
                break;
            case Direction.Left:
                MoveLeft();
                break;
            case Direction.Up:
                MoveUp();
                break;
            case Direction.Down:
                MoveDown();
                break;
        }

        CreateRandomTile();
    }

    private void CreateRandomTile()
    {
        var emptyTiles = new List<Vector2Int>();
        for (var x = 0; x < _length; x++)
        {
            for (var y = 0; y < _length; y++)
            {
                if (TileArray[x, y].IsEmpty)
                {
                    emptyTiles.Add(new Vector2Int(x, y));
                }
            }
        }

        if (emptyTiles.Count == 0) return;
        var randomIndex = Random.Range(0, emptyTiles.Count);
        var randomTile = emptyTiles[randomIndex];

        GameManager.Instance.GameLogic.PlayTileAction(new CrateTileTileAction(Index, randomTile.y, randomTile.x));
    }

    private void MoveRight()
    {
        for (var y = 0; y < _length; y++)
        {
            for (var x = _length - 1; x >= 0; x--)
            {
                if (TileArray[x, y].IsEmpty) continue;
                var nextX = x;
                while (nextX + 1 < _length)
                {
                    if (TileArray[nextX + 1, y].IsEmpty)
                    {
                        nextX++;
                    }
                    else
                    {
                        if (TileArray[nextX + 1, y].Score == TileArray[x, y].Score)
                        {
                            GameManager.Instance.GameLogic.PlayTileAction(new DestroyTileTileAction(Index, x, y));
                            GameManager.Instance.GameLogic.PlayTileAction(new MoveTileTileAction(Index, x, y, nextX + 1, y, true));
                            nextX++;
                        }

                        break;
                    }
                }

                if (nextX == x) continue;
                GameManager.Instance.GameLogic.PlayTileAction(new DestroyTileTileAction(Index, x, y));
                GameManager.Instance.GameLogic.PlayTileAction(new MoveTileTileAction(Index, x, y, nextX, y, false));
            }
        }
    }

    private void MoveLeft()
    {
        for (var y = 0; y < _length; y++)
        {
            for (var x = 0; x < _length; x++)
            {
                if (TileArray[x, y].IsEmpty) continue;
                var nextX = x;
                while (nextX - 1 >= 0)
                {
                    if (TileArray[nextX - 1, y].IsEmpty)
                    {
                        nextX--;
                    }
                    else
                    {
                        if (TileArray[nextX - 1, y].Score == TileArray[x, y].Score)
                        {
                            GameManager.Instance.GameLogic.PlayTileAction(new DestroyTileTileAction(Index, x, y));
                            GameManager.Instance.GameLogic.PlayTileAction(new MoveTileTileAction(Index, x, y, nextX - 1, y, true));
                            nextX--;
                        }

                        break;
                    }
                }

                if (nextX == x) continue;
                GameManager.Instance.GameLogic.PlayTileAction(new DestroyTileTileAction(Index, x, y));
                GameManager.Instance.GameLogic.PlayTileAction(new MoveTileTileAction(Index, x, y, nextX, y, false));
            }
        }
    }

    private void MoveUp()
    {
        for (var x = 0; x < _length; x++)
        {
            for (var y = 0; y < _length; y++)
            {
                if (TileArray[x, y].IsEmpty) continue;
                var nextY = y;
                while (nextY - 1 >= 0)
                {
                    if (TileArray[x, nextY - 1].IsEmpty)
                    {
                        nextY--;
                    }
                    else
                    {
                        if (TileArray[x, nextY - 1].Score == TileArray[x, y].Score)
                        {
                            GameManager.Instance.GameLogic.PlayTileAction(new DestroyTileTileAction(Index, x, y));
                            GameManager.Instance.GameLogic.PlayTileAction(new MoveTileTileAction(Index, x, y, x, nextY - 1, true));
                            nextY--;
                        }

                        break;
                    }
                }

                if (nextY == y) continue;
                GameManager.Instance.GameLogic.PlayTileAction(new DestroyTileTileAction(Index, x, y));
                GameManager.Instance.GameLogic.PlayTileAction(new MoveTileTileAction(Index, x, y, x, nextY, false));
            }
        }
    }

    private void MoveDown()
    {
        for (var x = _length - 1; x >= 0; x--)
        {
            for (var y = 0; y < _length; y++)
            {
                if (TileArray[x, y].IsEmpty) continue;
                var nextY = y;
                while (nextY + 1 < _length)
                {
                    if (TileArray[x, nextY + 1].IsEmpty)
                    {
                        nextY++;
                    }
                    else
                    {
                        if (TileArray[x, nextY + 1].Score == TileArray[x, y].Score)
                        {
                            GameManager.Instance.GameLogic.PlayTileAction(new DestroyTileTileAction(Index, x, y));
                            GameManager.Instance.GameLogic.PlayTileAction(new MoveTileTileAction(Index, x, y, x, nextY + 1, true));
                            nextY++;
                        }

                        break;
                    }
                }

                if (nextY == y) continue;
                GameManager.Instance.GameLogic.PlayTileAction(new DestroyTileTileAction(Index, x, y));
                GameManager.Instance.GameLogic.PlayTileAction(new MoveTileTileAction(Index, x, y, x, nextY, false));
            }
        }
    }
}