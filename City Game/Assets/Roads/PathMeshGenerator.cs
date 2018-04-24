using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public static class PathMeshGenerator
{
    const float resolution = 1f; // triangles per metre (Not that precise but it should do)
    const float small = 0.001f;

    public static Mesh Generate(Arc path, float width)
    {
        int subdivisions = (int)Mathf.Ceil(path.GetLength() * resolution);
        float distancePerSample = 0.5f / subdivisions;
        subdivisions++;

        Vector3[] points = new Vector3[subdivisions * 2];

        for (int i = 0; i < subdivisions * 2; i++)
        {
            float t = i * distancePerSample;

            Vector2 P1 = path.GetValue(t);
            Vector2 P2 = path.GetValue(t + small);

            Vector3 delta = (new Vector3(P1.x - P2.x, 0, P1.y - P2.y)).normalized;
            Vector3 norm = new Vector3(delta.z, delta.y, delta.x) * width;

            points[i] = new Vector3(P1.x, 0, P1.y) + norm;
            i++;

            points[i] = new Vector3(P1.x, 0, P1.y) - norm;

            Debug.Log(new Vector2(P1.x, P1.y));
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