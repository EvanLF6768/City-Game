using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public struct Spline
{
    public float a, b, c, d, shift;

    public Spline(Vector2 p1, Vector2 p2, float d1, float d2)
    {
        Vector2 newP2 = new Vector2(p2.x - p1.x, p2.y);
        shift = p2.x - p1.x;
        d = p1.y;
        c = d1;
        float x2 = newP2.x * newP2.x;
        a = -(2 * newP2.x * newP2.y - 2 * c * x2 + 2 * newP2.x * d - x2 * d2 + x2 * c) / (x2 * x2);
        b = (d2 - c - 3 * a * x2) / (2 * newP2.x);
    }

    public float getValue(float t)
    {
        t -= shift;
        return a * t * t * t + b * t * t + c * t + d;
    }
}