using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public partial class GameLogicBase
{
    private readonly List<TileAction> _commanderList = new List<TileAction>();
    private readonly List<Tween> _tweenList = new List<Tween>();

    public void PlayTileAction(TileAction action)
    {
        action.OnAction();
        if (IsVisible) action.OnGameObjectAction();
        _commanderList.Add(action);
    }

    public void AddTween(Tween tween)
    {
        CompleteTween();
        _tweenList.Add(tween);
    }

    private void SetTweenSpeed(float speed)
    {
        CompleteTween();
        foreach (var tween in _tweenList)
        {
            tween.timeScale = speed;
        }
    }

    private void CompleteTween()
    {
        for (var i = 0; i < _tweenList.Count; i++)
        {
            if (_tweenList[i].IsActive()) continue;
            _tweenList.RemoveAt(i);
            i--;
        }
    }
}