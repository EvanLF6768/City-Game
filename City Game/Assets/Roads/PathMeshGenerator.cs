using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public static class PathMeshGenerator
{
    const int subdivisions = 6;

    public static Vector2[] Subdivide(Vector2[] points, int iterations)
    {
        if (iterations == 0) return points;

        Vector2[] newPoints = new Vector2[points.Length * 2 - 1];
        newPoints[0] = points[0];
        newPoints[1] = Vector2.Lerp(points[0], points[1], 2f / 3f);

        for (int i = 1; i < points.Length - 1; i++)
        {
            newPoints[i * 2] = Vector2.Lerp(points[i], points[i + 1], 1f / 3f);
            newPoints[i * 2 + 1] = Vector2.Lerp(points[i], points[i + 1], 2f / 3f);
        }

        newPoints[points.Length * 2 - 2] = points[points.Length - 1];

        return Subdivide(newPoints, iterations - 1);
    }

    public static Vector3[] AddThirdDimension(Vector2[] points, Func<float, float> function)
    {
        Vector3[] output = new Vector3[points.Length];

        for (int i = 0; i < points.Length; i++)
        {
            output[i] = new Vector3(points[i].x, function((float)i / (float)points.Length), points[i].y);
        }

        return output;
    }

    public static Mesh Generate(PathInfo path, float width)
    {
        Vector3[] pointsLHS = AddThirdDimension(Subdivide(new Vector2[] { path.GetOffsetStart(width / 2), path.GetOffsetMiddle(width / 2), path.GetOffsetEnd(width / 2) }, subdivisions), path.Height.getValue);
        Vector3[] pointsRHS = AddThirdDimension(Subdivide(new Vector2[] { path.GetOffsetStart(-width / 2), path.GetOffsetMiddle(-width / 2), path.GetOffsetEnd(-width / 2) }, subdivisions), path.Height.getValue);
        Vector3[] points = new Vector3[pointsLHS.Length + pointsRHS.Length];

        for (int i = 0; i < pointsLHS.Length; i++)
        {
            points[i * 2] = pointsLHS[i];
            points[i * 2 + 1] = pointsRHS[i];
        }

        int[] tris = new int[(points.Count() - 2) * 6];
        int c = 0;

        for (int i = 0; i < points.Count() - 2; i++)
        {
            tris[c++] = i;
            tris[c++] = i + 1;
            tris[c++] = i + 2;
            tris[c++] = i + 2;
            tris[c++] = i + 1;
            tris[c++] = i++;

            tris[c++] = i;
            tris[c++] = i + 1;
            tris[c++] = i + 2;
            tris[c++] = i + 2;
            tris[c++] = i + 1;
            tris[c++] = i;
        }

        Mesh mesh = new Mesh();
        mesh.Clear();
        mesh.SetVertices(new List<Vector3>(points));
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }
}