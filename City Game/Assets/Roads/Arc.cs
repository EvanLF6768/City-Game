using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public struct Arc
{
    public Vector2 T { get; private set; }
    float S, L, R, M;

    public Arc(Vector2 D1, Vector2 D2, Vector2 P1, Vector2 P2)
    {
        if (D1.y == 0)
        {
            T = new Vector2(P2.x + P2.y * D2.y / D2.x, 0);
        }
        else if (D2.y == 0)
        {
            T = new Vector2(P1.x + P1.y * D1.y / D1.x, 0);
        }
        else
        {
            float M1 = D1.x / D1.y;
            float M2 = D2.x / D2.y;
            float Cx = (P2.y - P1.y + P1.x * M1 - P2.x * M2) / (M1 - M2);

            T = new Vector2(Cx, P1.y + M1 * (Cx - P1.x));
        }

        P1 -= T;
        P2 -= T;

        if (P1.magnitude < 0.01)
        {
            S = Mathf.Atan2(T.y, T.x);
        }
        else
        {
            S = Mathf.Atan2(P1.y, P1.x);
        }

        if (P2.magnitude < 0.01)
        {
            L = Mathf.Atan2(T.y, T.x);
        }
        else
        {
            L = Mathf.Atan2(P2.y, P2.x);
        }

        R = P1.magnitude;
        M = 1;

        Debug.Log(T);
        Debug.Log(S);
        Debug.Log(L);
        Debug.Log(R);
        Debug.Log(M);
    }

    public Vector2 GetValue(float t)
    {
        return new Vector2(R * Mathf.Cos(t * L + S) + T.x, R * M * Mathf.Sin(t * L + S) + T.y);
    }

    public float GetLength()
    {
        return Mathf.Abs(L) * R;
    }
}