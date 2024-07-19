using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PublicGameHelper
{
    private const int LENGTH = 180;
    private const int START_X = 145;
    private const int START_Y = -145;
    
    public static Vector2 GetPosition(int x, int y)
    {
        var X = START_X + x * LENGTH;
        var Y = START_Y - y * LENGTH;
        return new Vector2(X, Y);
    }
    
    public static void SetPosition(GameObject go, int x, int y)
    {
        var rectTransform = go.GetComponent<RectTransform>();
        if (rectTransform == null) return;
        var X = START_X + x * LENGTH;
        var Y = START_Y - y * LENGTH;
        rectTransform.anchoredPosition = new Vector2(X, Y);
    }
    
    public static void SetTextScore(GameObject go, long score)
    {
        var text = go.GetComponent<TextMesh>();
        if (text == null) return;
        text.text = score.ToString();
    }

    public static void SetColorScore(GameObject go, long score)
    {
        if (GameManager.Instance.TileColor_SO == null) return;
        var image = go.GetComponent<Image>();
        if (image == null) return;
        image.color = GameManager.Instance.TileColor_SO.TileColorDict[score];
    }
}
