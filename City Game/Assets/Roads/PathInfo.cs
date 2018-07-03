using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public struct PathInfo
{
    public Vector2 Start;
    public Vector2 Middle;
    public Vector2 End;
    public Spline Height;

    public PathInfo(Vector2 start, Vector2 middle, Vector2 end, Spline height)
    {
        Start = start;
        Middle = middle;
        End = end;
        Height = height;
    }

    public Vector2 GetOffsetStart(float offset)
    {
        return Start + new Vector2(Middle.y - Start.y, Middle.x - Start.x).normalized * offset;
    }

    public Vector2 GetOffsetEnd(float offset)
    {
        return End + new Vector2(Middle.y - End.y, Middle.x - End.x).normalized * offset;
    }

    public Vector2 GetOffsetMiddle(float offset)
    {
        if (Mathf.Abs(Start.x - Middle.x) < 0.001f)
        {
            float M = (End.y - Middle.y) / (End.x - Middle.x);
            return new Vector2(Start.x, End.y + M * (End.x - Start.x));
        }

        if (Mathf.Abs(End.x - Middle.x) < 0.001f)
        {
            float M = (Start.y - Middle.y) / (Start.x - Middle.x);
            return new Vector2(End.x, Start.y + M * (Start.x - End.x));
        }

        float M1 = (Start.y - Middle.y) / (Start.x - Middle.x);
        float M2 = (End.y - Middle.y) / (End.x - Middle.x);

        float x = (M1 * Start.x - M2 * End.x - Start.y + End.y) / (M1 - M2);
        float y = Start.y + M1 * (x - Start.x);
        return new Vector2(x, y);
    }
}