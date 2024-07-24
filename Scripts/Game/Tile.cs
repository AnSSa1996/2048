using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile
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

public class MoveTileTileAction : ITileAction
{
    public long Index { get; set; }
    private readonly int _fromY;
    private readonly int _fromX;
    private readonly int _toY;
    private readonly int _toX;
    private readonly bool _isMerged;

    private long _score = 0;
    
    private Tween _fromTileTweenMoveAnimation;
    private Tween _fromTileTweenImageColorAnimation;
    private Tween _fromTileTweenTextColorAnimation;
    private Tween _toTileTweenScaleAnimation;

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
        var fromTile = GameManager.Instance.GameLogic.Board.TileArray[_fromY, _fromX];
        var toTile = GameManager.Instance.GameLogic.Board.TileArray[_toY, _toX];
        _score = fromTile.Score;
        if (_isMerged) _score *= 2;
        fromTile.Clear();
        toTile.SetScore(_score);
    }

    public void OnGameObjectAction()
    {
        if (_isMerged)
        {
            MergeAction();
        }
        else
        {
            MoveAction();
        }
    }
    
    public void OnGameObjectSkipAction()
    {
        if (_fromTileTweenMoveAnimation == null) return;
        _fromTileTweenMoveAnimation.Complete();
        if (_fromTileTweenImageColorAnimation == null) return;
        _fromTileTweenImageColorAnimation.Complete();
        if (_fromTileTweenTextColorAnimation == null) return;
        _fromTileTweenTextColorAnimation.Complete();
        if (_toTileTweenScaleAnimation == null) return;
        _toTileTweenScaleAnimation.Complete();
        SetDisplay();
    }

    private void MoveAction()
    {
        FromTileMoveTween();
        SetFromTileToTile();
    }

    private void MergeAction()
    {
        FromTileMoveTween();
        FromTileImageTween();
        FromTileTextColorTween();
        ToTileScaleTween();
    }

    private void SetFromTileToTile()
    {
        var fromTile = GameManager.Instance.GameLogic.Board.TileGameObjectArray[_fromY, _fromX];
        GameManager.Instance.GameLogic.Board.TileGameObjectArray[_fromY, _fromX] = null;
        GameManager.Instance.GameLogic.Board.TileGameObjectArray[_toY, _toX] = fromTile;
    }

    private void FromTileMoveTween()
    {
        var fromTile = GameManager.Instance.GameLogic.Board.TileGameObjectArray[_fromY, _fromX];
        if (fromTile == null) return;
        var rectTransform = fromTile.GetComponent<RectTransform>();
        if (rectTransform == null) return;
        _fromTileTweenMoveAnimation = rectTransform.DOLocalMove(PublicGameHelper.GetPosition(_toY, _toX), GameLogicBase.ACTION_MOVE_TIME).SetEase(Ease.Linear).Play();
        GameManager.Instance.GameLogic.AddTween(_fromTileTweenMoveAnimation);
    }

    private void FromTileImageTween()
    {
        var fromTile = GameManager.Instance.GameLogic.Board.TileGameObjectArray[_fromY, _fromX];
        if (fromTile == null) return;
        var image = fromTile.GetComponent<Image>();
        if (image == null) return;
        var currentColor = image.color;
        _fromTileTweenImageColorAnimation = image.DOColor(new Color(currentColor.r, currentColor.g, currentColor.b, 0), GameLogicBase.ACTION_MOVE_TIME).Play();
        GameManager.Instance.GameLogic.AddTween(_fromTileTweenImageColorAnimation);
    }

    private void FromTileTextColorTween()
    {
        var fromTile = GameManager.Instance.GameLogic.Board.TileGameObjectArray[_fromY, _fromX];
        if (fromTile == null) return;
        var text = fromTile.GetComponentInChildren<TextMeshProUGUI>();
        if (text == null) return;
        var currentTextColor = text.color;
        _fromTileTweenTextColorAnimation = text.DOColor(new Color(currentTextColor.r, currentTextColor.g, currentTextColor.b, 0), GameLogicBase.ACTION_MOVE_TIME).Play();
        GameManager.Instance.GameLogic.AddTween(_fromTileTweenTextColorAnimation);
    }

    private void ToTileScaleTween()
    {
        var toTile = GameManager.Instance.GameLogic.Board.TileGameObjectArray[_toY, _toX];
        if (toTile == null) return;
        _toTileTweenScaleAnimation = toTile.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), GameLogicBase.ACTION_MERGE_TIME)
            .SetEase(Ease.Linear)
            .SetLoops(2, LoopType.Yoyo)
            .OnStepComplete(SetDisplay)
            .Play();
        GameManager.Instance.GameLogic.AddTween(_toTileTweenScaleAnimation);
    }
    
    private void SetDisplay()
    {
        PublicGameHelper.SetTextScore(GameManager.Instance.GameLogic.Board.TileGameObjectArray[_toY, _toX], _score);
        PublicGameHelper.SetTextColorInit(GameManager.Instance.GameLogic.Board.TileGameObjectArray[_toY, _toX]);
        PublicGameHelper.SetColorScore(GameManager.Instance.GameLogic.Board.TileGameObjectArray[_toY, _toX], _score);
    }
}

public class CrateTileTileAction : ITileAction
{
    public long Index { get; set; }
    private readonly int Y;
    private readonly int X;
    
    private Tween _tweenAnimation;

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
        tileGameObject.transform.localScale = new Vector3(0, 0, 0);
        _tweenAnimation = tileGameObject.transform.DOScale(new Vector3(1, 1, 1), GameLogicBase.ACTION_TIME).SetEase(Ease.OutBounce).Play();
        GameManager.Instance.GameLogic.AddTween(_tweenAnimation);

        PublicGameHelper.SetPosition(tileGameObject, Y, X);
        PublicGameHelper.SetTextScore(tileGameObject, 2);
        PublicGameHelper.SetTextColorInit(tileGameObject);
        PublicGameHelper.SetColorScore(tileGameObject, 2);
    }
    
    public void OnGameObjectSkipAction()
    {
        if (_tweenAnimation == null) return;
        _tweenAnimation.Complete();
    }
}

public class DestroyTileTileAction : ITileAction
{
    public long Index { get; set; }
    private readonly int Y;
    private readonly int X;
    
    private Tween _tweenScaleAnimation;

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
        _tweenScaleAnimation = tileGameObject.transform.DOScale(new Vector3(0, 0, 0), 0f).SetEase(Ease.OutBounce);
        _tweenScaleAnimation.SetDelay(GameLogicBase.ACTION_TIME);
        _tweenScaleAnimation.OnComplete(() => { tileGameObject.ReleaseGameObject(); });
        _tweenScaleAnimation.Play();
        GameManager.Instance.GameLogic.AddTween(_tweenScaleAnimation);
    }
    
    public void OnGameObjectSkipAction()
    {
        if (_tweenScaleAnimation == null) return;
        _tweenScaleAnimation.Complete();
    }
}