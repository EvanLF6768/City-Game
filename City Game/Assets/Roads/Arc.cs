using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public struct Arc
{
    public Vector2 C { get; private set; }
    float S, L, R, M;

    public static Arc ThreePoint(Vector2 P1, Vector2 P2, Vector2 P3)
    {
        return new Arc(P2 - P1, P3 - P2, P1, P3);
    }

    public Arc(Vector2 start, Vector2 centre, Vector2 end)
    {
        M = 1;
        C = centre;
        R = (start - centre).magnitude;
        S = Mathf.Atan2(start.y - centre.y, start.x - centre.x);
        L = Mathf.Atan2(end.y - centre.y, end.x - centre.x) - S;

        L = L > Mathf.PI ? L - 2 * Mathf.PI : L;
    }

    public Arc(Vector2 D1, Vector2 D2, Vector2 P1, Vector2 P2)
    {
        if (D1.y == 0)
        {
            C = new Vector2(0, P2.x + P2.y * D2.x / D2.y);
        }
        else if (D2.y == 0)
        {
            C = new Vector2(0, P1.x + P1.y * D1.x / D1.y);
        }
        else
        {   
            float M1 = -D1.x / D1.y;
            float M2 = -D2.x / D2.y;
            float Cx = (P2.y - P1.y + P1.x * M1 - P2.x * M2) / (M1 - M2);

            C = new Vector2(Cx, P1.y + M1 * (Cx - P1.x));
        }

        P1 -= C;
        P2 -= C;

        
        S = Mathf.Atan2(P1.y, P1.x);    
        L = Mathf.Atan2(P2.y, P2.x) - S;

        R = P1.magnitude;
        M = 1;

        Debug.Log(C);
        Debug.Log(S);
        Debug.Log(L);
        Debug.Log(R);
        Debug.Log(M);
    }

    public Vector2 GetValue(float t)
    {
        return new Vector2(R * Mathf.Cos(t * L + S) + C.x, R * M * Mathf.Sin(t * L + S) + C.y);
    }

    public float GetLength()
    {
        return Mathf.Abs(L) * R;
    }

    public Vector2 GetNormal(float t)
    {
        return new Vector2(R * Mathf.Cos(t * L + S), R * M * Mathf.Sin(t * L + S));
    }
}