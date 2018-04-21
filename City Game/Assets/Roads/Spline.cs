using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public struct Spline
{
    Parametic x, y, z;

    public Spline(Vector3 startVal, Vector3 startDir, Vector3 endVal, Vector3 endDir)
    {
        x = new Parametic(startVal.x, startDir.x, endVal.x, endDir.x);
        y = new Parametic(startVal.y, startDir.y, endVal.y, endDir.y);
        z = new Parametic(startVal.x, startDir.x, endVal.x, endDir.x);
    }

    public Vector3 GetValue(float t)
    {
        return new Vector3(x.GetValue(t), y.GetValue(t), z.GetValue(t));
    }

    public Vector3 GetTangent(float t)
    {
        return new Vector3(x.GetTangent(t), y.GetTangent(t), z.GetTangent(t));
    }

    public Vector3 GetNormalXZ(float t)
    {
        return new Vector3(x.GetNormal(t), y.GetTangent(t), z.GetNormal(t));
    }

    public struct Parametic
    {
        float a, b, c, d;

        public Parametic(float startVal, float startDir, float endVal, float endDir)
        {
            a = 2 * startVal + startDir + endDir - 2 * endVal;
            b = 3 * endVal - 3 * startVal - 2 * startDir - endDir;
            c = startDir;
            d = startVal;
        }

        public float GetValue(float t)
        {
            return ((a * t + b) * t + c) * t + d;
        }

        public float GetTangent(float t)
        {
            return (3 * a * t + 2 * b) * t + c;
        }

        public float GetNormal(float t)
        {
            return -1f / GetTangent(t);
        }
    }
}