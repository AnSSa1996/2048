using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameLogicBase
{
    
    public static readonly float ACTION_TIME = 0.5f;
    public static readonly float ACTION_MOVE_TIME = ACTION_TIME * 0.5f;
    public static readonly float ACTION_MERGE_TIME = ACTION_TIME * 0.4f;

    private const int BOARD_LENGTH = 5;
    private const float SWIPE_THRESHOLD = 0.05f;

    private int _gameSpeed = 1;
    
    private Vector2 _startingTouch = Vector2.zero;
    private bool _isSwiping = false;
    
    public Board Board { get; private set; } = null;
    private bool IsVisible { get; set; } = true;
    private bool IsGameOver { get; set; } = false;
    private bool IsRandomMove { get; set; } = false;

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
        
        GameSpeed = 1;
        
        _startingTouch = Vector2.zero;
        _isSwiping = false;

        IsGameOver = false;
    }

    public void Update(float time)
    {
        var deltaTime = time * GameSpeed;
        UpdateMove();
        UpdateRandomMove();
    }

    public void Destroy()
    {
    }

    public void GameOver()
    {
        IsGameOver = true;
    }

    public void SetGameSpeed(int speed)
    {
        GameSpeed = speed;
    }
    
    public void RandomMove()
    {
        IsRandomMove = !IsRandomMove;
    }

    private void UpdateMove()
    {
        MouseMove();
        MouseMoveCheck();
#if UNITY_EDITOR
        KeyMoveCheck();
#endif
    }
    
    private void UpdateRandomMove()
    {
        if (IsRandomMove == false) return;
        if (IsGameOver) return;
        var randDirection = (Direction) Random.Range(0, 4);
        MoveDirection(randDirection);
    }

    private void MouseMove()
    {
        if (GameManager.Instance.GetMouseOrTouch().IsDown)
        {
            _startingTouch = GameManager.Instance.GetMouseOrTouch().Position;
            _isSwiping = true;
        }

        if (GameManager.Instance.GetMouseOrTouch().IsUp)
        {
            _isSwiping = false;
        }
    }

    private void MouseMoveCheck()
    {
        if (_isSwiping == false) return;
        var diff = GameManager.Instance.GetMouseOrTouch().Position - _startingTouch;
        diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);
        if (diff.x < -SWIPE_THRESHOLD)
        {
            MoveDirection(Direction.Left);
            return;
        }

        if (diff.x > SWIPE_THRESHOLD)
        {
            MoveDirection(Direction.Right);
            return;
        }

        if (diff.y < -SWIPE_THRESHOLD)
        {
            MoveDirection(Direction.Down);
            return;
        }

        if (diff.y > SWIPE_THRESHOLD)
        {
            MoveDirection(Direction.Up);
            return;
        }
    }
    
    private void KeyMoveCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveDirection(Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveDirection(Direction.Right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveDirection(Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDirection(Direction.Down);
        }
    }

    private void MoveDirection(Direction direction)
    {
        _isSwiping = false;
        SkipCommander();
        Board.Move(direction);
    }
}