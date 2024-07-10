using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicBase
{
    public static int BOARD_LENGTH = 5;
    public Board Board;
    
    public void Init()
    {
        Board = new Board(BOARD_LENGTH, BOARD_LENGTH);
    }

    public void Update(float time)
    {
        
    }

    public void Destroy()
    {
        
    }
}
