using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : Singleton<GameManager>
{
    private readonly InputWrapper _inputSystem = new InputWrapper();

    public MouseOrTouch GetMouseOrTouch()
    {
        return _inputSystem.GetMouseOrTouch();
    }

    public void LongTouchComplete()
    {
        _inputSystem.LongTouchComplete();
    }
}

public struct MouseOrTouch
{
    public Vector2 Position;
    public bool IsDown;
    public bool IsUp;
    public bool IsDrag;
    public bool IsLongPressed;

    public float touchStartTime;
    public bool longPressActivated;
}

public class InputWrapper
{
    private MouseOrTouch mouseOrTouch;

    public void LongTouchComplete()
    {
        if (mouseOrTouch.IsLongPressed)
        {
            mouseOrTouch.IsLongPressed = false;
        }
    }

    public MouseOrTouch GetMouseOrTouch()
    {
        mouseOrTouch.IsDown = false;
        mouseOrTouch.IsUp = false;
        mouseOrTouch.IsDrag = false;
        mouseOrTouch.Position = Input.mousePosition;
        
        void UNITY_EDITOR_TOUCH()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseOrTouch.touchStartTime = Time.time;
                mouseOrTouch.IsLongPressed = false;
                mouseOrTouch.longPressActivated = false;
                mouseOrTouch.IsDown = true;
                mouseOrTouch.IsUp = false;
            }

            if (Input.GetMouseButtonUp(0))
            {
                mouseOrTouch.touchStartTime = 0f;
                mouseOrTouch.IsLongPressed = false;
                mouseOrTouch.longPressActivated = false;
                mouseOrTouch.IsDown = false;
                mouseOrTouch.IsUp = true;
            }

            if (mouseOrTouch.longPressActivated == false && Input.GetMouseButton(0) && (Time.time - mouseOrTouch.touchStartTime) >= 0.5f)
            {
                mouseOrTouch.IsLongPressed = true;
                mouseOrTouch.longPressActivated = true; // 롱터치 발동을 기록
            }

            if (Input.GetMouseButton(0))
            {
                mouseOrTouch.IsDrag = true;
            }
            else
            {
                mouseOrTouch.IsDrag = false;
            }
        }
        
        void MOBILE_TOUCH()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                mouseOrTouch.Position = touch.position;

                if (touch.phase == TouchPhase.Began)
                {
                    mouseOrTouch.touchStartTime = Time.time; // 터치 시작 시간 기록
                    mouseOrTouch.IsLongPressed = false;
                    mouseOrTouch.longPressActivated = false; // 롱터치 발동 초기화
                    mouseOrTouch.IsDown = true;
                    mouseOrTouch.IsUp = false;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    mouseOrTouch.touchStartTime = 0f;
                    mouseOrTouch.IsLongPressed = false;
                    mouseOrTouch.longPressActivated = false;
                    mouseOrTouch.IsDown = false;
                    mouseOrTouch.IsUp = true;
                }

                if (mouseOrTouch.longPressActivated == false && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) && (Time.time - mouseOrTouch.touchStartTime) >= 0.5f)
                {
                    mouseOrTouch.IsLongPressed = true;
                    mouseOrTouch.longPressActivated = true; // 롱터치 발동을 기록
                }

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    mouseOrTouch.IsDrag = true;
                }
                else
                {
                    mouseOrTouch.IsDrag = false;
                }
            }
        }

#if UNITY_EDITOR
        UNITY_EDITOR_TOUCH();
#else
        MOBILE_TOUCH();
#endif

        return mouseOrTouch;
    }
}