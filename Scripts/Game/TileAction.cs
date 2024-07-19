using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TileAction
{
    public long Index { get; set; }
    void OnAction();
    void OnGameObjectAction();
}