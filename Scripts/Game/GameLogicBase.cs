using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameLogicBase
{
    public static readonly float ACTION_TIME = 0.5f;
    private static readonly int BOARD_LENGTH = 5;
    public bool IsVisible { get; set; }
    public Board Board { get; private set; }
    
    private int _gameSpeed;

    public int GameSpeed
    {
        get => _gameSpeed;
        private set
        {
            if (_gameSpeed == value) return;
            _gameSpeed = value;
            SetTweenSpeed(_gameSpeed);
        }
    }

    public void Init()
    {
        IsVisible = true;
        Board = new Board(BOARD_LENGTH);
        Board.Init();
    }

    public void Update(float time)
    {
        
    }

    public void Destroy()
    {
        
    }
    
    public void SetGameSpeed(int speed)
    {
        GameSpeed = speed;
    }
}
