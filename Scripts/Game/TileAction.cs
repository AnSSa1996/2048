using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileAction
{
    public long Index { get; set; }
    void OnAction();
    void OnGameObjectAction();
    void OnGameObjectSkipAction();
}