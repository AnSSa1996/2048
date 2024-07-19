using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public struct Tile
{
    public long Score;
    public bool IsEmpty;

    public Tile(long score, bool isEmpty)
    {
        Score = score;
        IsEmpty = isEmpty;
    }

    public void SetScore(long score)
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

public class MoveTileTileAction : TileAction
{
    public long Index { get; set; }
    private readonly int _fromY;
    private readonly int _fromX;
    private readonly int _toY;
    private readonly int _toX;
    private readonly bool _isMerged;

    private long _score = 0;

    public MoveTileTileAction(long index, int fromY, int fromX, int toY, int toX, bool isMerged)
    {
        Index = index;
        _fromX = fromX;
        _fromY = fromY;
        _toX = toX;
        _toY = toY;
        _isMerged = isMerged;
    }

    public void OnAction()
    {
        var fromTile = GameManager.Instance.GameLogic.Board.TileArray[_toY, _toX];
        var toTile = GameManager.Instance.GameLogic.Board.TileArray[_fromY, _fromX];
        _score = fromTile.Score;
        if (_isMerged) _score *= 2;
        toTile.SetScore(_score);
    }

    public void OnGameObjectAction()
    {
        var fromTile = GameManager.Instance.GameLogic.Board.TileGameObjectArray[_fromY, _fromX];
        if (fromTile == null) return;
        var rectTransform = fromTile.GetComponent<RectTransform>();
        if (rectTransform == null) return;
        var fromTileTweenAnimation = rectTransform.DOMove(PublicGameHelper.GetPosition(_toX, _toY), GameLogicBase.ACTION_TIME).SetEase(Ease.OutBounce).Play();
        GameManager.Instance.GameLogic.AddTween(fromTileTweenAnimation);

        var toTile = GameManager.Instance.GameLogic.Board.TileGameObjectArray[_toY, _toX];
        if (toTile == null) return;
        var toTileTweenAnimation = toTile.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), GameLogicBase.ACTION_TIME / 2f)
            .SetEase(Ease.OutBounce)
            .SetLoops(2, LoopType.Yoyo)
            .OnComplete(SetDisplay)
            .Play();

        GameManager.Instance.GameLogic.AddTween(toTileTweenAnimation);
    }

    private void SetDisplay()
    {
        PublicGameHelper.SetTextScore(GameManager.Instance.GameLogic.Board.TileGameObjectArray[_toY, _toX], _score);
        PublicGameHelper.SetColorScore(GameManager.Instance.GameLogic.Board.TileGameObjectArray[_toY, _toX], _score);
    }
}

public class CrateTileTileAction : TileAction
{
    public long Index { get; set; }
    private readonly int Y;
    private readonly int X;

    public CrateTileTileAction(long index, int y, int x)
    {
        Index = index;
        Y = y;
        X = x;
    }

    public void OnAction()
    {
        var tile = GameManager.Instance.GameLogic.Board.TileArray[Y, X];
        tile.SetScore(2);
    }

    public void OnGameObjectAction()
    {
        var tileGameObject = ResourceManager.SpawnGameObject("Prefabs/Tile");
        if (tileGameObject == null) return;
        GameManager.Instance.GameLogic.Board.TileGameObjectArray[Y, X] = tileGameObject;
        tileGameObject.transform.SetParent(GameManager.Instance.TileParent);
        tileGameObject.transform.localScale = new Vector3(0,0,0);
        var tweenAnimation = tileGameObject.transform.DOScale(new Vector3(1, 1, 1), GameLogicBase.ACTION_TIME).SetEase(Ease.OutBounce).Play();
        GameManager.Instance.GameLogic.AddTween(tweenAnimation);
        
        PublicGameHelper.SetPosition(tileGameObject, X, Y);
        PublicGameHelper.SetTextScore(tileGameObject, 2);
        PublicGameHelper.SetColorScore(tileGameObject, 2);
    }
}

public class DestroyTileTileAction : TileAction
{
    public long Index { get; set; }
    private readonly int Y;
    private readonly int X;

    public DestroyTileTileAction(long index, int y, int x)
    {
        Index = index;
        Y = y;
        X = x;
    }

    public void OnAction()
    {
        GameManager.Instance.GameLogic.Board.TileArray[Y, X].Clear();
    }

    public void OnGameObjectAction()
    {
        var tileGameObject = GameManager.Instance.GameLogic.Board.TileGameObjectArray[Y, X];
        if (tileGameObject == null) return;
        GameManager.Instance.GameLogic.Board.TileGameObjectArray[Y, X] = null;
        var tweenAnimation = tileGameObject.transform.DOScale(new Vector3(0, 0, 0), 0f).SetEase(Ease.OutBounce);
        tweenAnimation.SetDelay(GameLogicBase.ACTION_TIME);
        tweenAnimation.OnComplete(() => { tileGameObject.ReleaseGameObject(); });
        tweenAnimation.Play();
        GameManager.Instance.GameLogic.AddTween(tweenAnimation);
    }
}