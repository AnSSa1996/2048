using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public partial class GameLogicBase
{
    private readonly List<ITileAction> _commanderList = new List<ITileAction>();
    private readonly List<Tween> _tweenList = new List<Tween>();

    public void PlayTileAction(ITileAction action)
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

    private void SkipCommander()
    {
        var preCommanderList = _commanderList.FindAll(x => x.Index == GameManager.Instance.GameLogic.Board.Index);
        if (preCommanderList.Count == 0) return;
        preCommanderList.ForEach(x => x.OnGameObjectSkipAction());
        _commanderList.Clear();
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