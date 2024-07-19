using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Transform TileParent;
    public TileColor_SO TileColor_SO;

    public GameLogicBase GameLogic { get; private set; }

    public void Start()
    {
        Init();
    }

    public void Update()
    {
        GameLogic.Update(Time.deltaTime);
    }

    private void Init()
    {
        InitTileParent();
        InitGameLogic();
        InitTileColor();
    }

    private void InitTileParent()
    {
        TileParent = GameObject.FindWithTag("TileParent").transform;
        if (TileParent == null)
        {
            Debug.LogError("TileParent를 찾을 수 없습니다.");
        }
    }

    private void InitGameLogic()
    {
        GameLogic = new GameLogicBase();
        GameLogic.Init();
    }

    private void InitTileColor()
    {
        TileColor_SO = Resources.Load<TileColor_SO>($"ScriptableObjects/TileColor");
        if (TileColor_SO == null)
        {
            Debug.LogError("TileColor_SO 로드에 실패했습니다.");
        }
    }
}