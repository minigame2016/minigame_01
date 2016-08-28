using UnityEngine;
using System.Collections;

public class GameEventManager : IEvent
{
    private uint key = 0;
    private object param1 = null;
    private object param2 = null;

    public GameEventManager(uint k, object p1, object p2)
    {
        key = k;
        param1 = p1;
        param2 = p2;
    }

    public uint GetKey()
    {
        return key;
    }
    public object GetParam1()
    {
        return param1;
    }
    public object GetParam2()
    {
        return param2;
    }
}

public class CameraMoveEvent : System.Object
{
    public float deltaPixelX;
    public float deltaPixelY;
    public CameraMoveEvent(float X, float Y)
    {
        deltaPixelX = X;
        deltaPixelY = Y;
    }
}

public class TouchEndEvent : System.Object
{
    public float DeltaX;
    public float DeltaY;
    public float TouchStartTime;
    public float TouchEndTime;
    public TouchEndEvent(float X, float Y, float startTime, float endTime)
    {
        DeltaX = X;
        DeltaY = Y;
        TouchStartTime = startTime;
        TouchEndTime = endTime;
    }
}

public class TouchLeftMostPosEvent : System.Object
{
    public float x;
    public float y;

    public TouchLeftMostPosEvent(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
}

public class KeyCodeEvent : System.Object
{
    public KeyCode keyCode;
    public KeyCodeEvent(KeyCode keycode)
    {
        keyCode = keycode;
    }
}
