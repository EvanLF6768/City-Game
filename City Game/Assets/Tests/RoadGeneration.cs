using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGeneration : MonoBehaviour
{
    public Material mat;

    public Vector2 D1, D2, P1, P2;

	void Start ()
    {
        Mesh msh = PathMeshGenerator.Generate(new Arc(D1, D2, P1, P2), 1.5f);
        Debug.Log(msh.vertexCount);

        var road = new GameObject("Road");

        var filt = road.AddComponent<MeshFilter>();
        filt.mesh = msh;

        var rend = road.AddComponent<MeshRenderer>();
        rend.material = mat;
	}
	
	void Update ()
    {
		
	}
}
