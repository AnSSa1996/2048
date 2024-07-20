using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameLogicBase
{
    public static readonly float ACTION_TIME = 0.5f;

    private const int BOARD_LENGTH = 5;
    private const float SWIPE_THRESHOLD = 0.2f;


    public bool IsVisible { get; set; }
    public Board Board { get; private set; }

    private int _gameSpeed;

    private Vector2 _startingTouch;
    private bool _isSwiping;

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
        MouseMove();
        MouseSwippingCheck();
    }

    public void Destroy()
    {
    }

    public void SetGameSpeed(int speed)
    {
        GameSpeed = speed;
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

    private void MouseSwippingCheck()
    {
        if (_isSwiping == false) return;
        var diff = GameManager.Instance.GetMouseOrTouch().Position - _startingTouch;
        diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);
        if (diff.x < -SWIPE_THRESHOLD)
        {
            MouseMoveDirection(Direction.Left);
            return;
        }

        if (diff.x > SWIPE_THRESHOLD)
        {
            MouseMoveDirection(Direction.Right);
            return;
        }

        if (diff.y < -SWIPE_THRESHOLD)
        {
            MouseMoveDirection(Direction.Down);
            return;
        }

        if (diff.y > SWIPE_THRESHOLD)
        {
            MouseMoveDirection(Direction.Up);
            return;
        }
    }

    private void MouseMoveDirection(Direction direction)
    {
        _isSwiping = false;
        Board.Move(direction);
    }
}