using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGeneration : MonoBehaviour
{
    public GameObject Guide1, Guide2, RoadGuide, PointGuide;

    public Material mat;

    Vector2 P1, P2, P3;
    int currentVector;
    
    void Update()
    {
        RaycastHit info = new RaycastHit();
        bool next = Input.GetMouseButtonDown(0) & Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out info);
        switch (currentVector)
        {
            case 0:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    P1 = new Vector2(info.point.x, info.point.z);
                }
                else
                {
                    P1 = new Vector2(Mathf.Round(info.point.x), Mathf.Round(info.point.z));
                }
                if (next)
                {
                    currentVector++;
                    Guide1.SetActive(true);
                }
                PointGuide.transform.position = new Vector3(P1.x, 0.01f, P1.y);
                break;

            case 1:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    P2 = new Vector2(info.point.x, info.point.z);
                }
                else
                {
                    P2 = new Vector2(Mathf.Round(info.point.x), Mathf.Round(info.point.z));
                }
                if (next)
                {
                    currentVector++;
                    Guide2.SetActive(true);
                    RoadGuide.SetActive(true);
                }
                Guide1.transform.position = new Vector3(P1.x, 0, P1.y) + (new Vector3(P2.x - P1.x, 0.01f, P2.y - P1.y) / 2);
                Guide1.transform.localScale = new Vector3(Guide1.transform.lossyScale.x, 1, (P1 - P2).magnitude / 10);
                Guide1.transform.LookAt(new Vector3(P2.x, 0, P2.y));

                PointGuide.transform.position = new Vector3(P2.x, 0.01f, P2.y);
                break;

            case 2:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    P3 = new Vector2(info.point.x, info.point.z);
                }
                else
                {
                    P3 = new Vector2(Mathf.Round(info.point.x), Mathf.Round(info.point.z));
                }
                if (next)
                {
                    Mesh mshj = PathMeshGenerator.Generate(new PathInfo(P1, P2, P3, new Spline(new Vector2(0,0), new Vector2(1,0), 0, 0)), 3f);

                    var road = new GameObject("Road");

                    var filjt = road.AddComponent<MeshFilter>();
                    filjt.mesh = mshj;

                    var rend = road.AddComponent<MeshRenderer>();
                    rend.material = mat;
                    currentVector = 0;

                    Guide1.SetActive(false);
                    Guide2.SetActive(false);
                    RoadGuide.SetActive(false);

                    break;
                }
                Guide2.transform.position = new Vector3(P3.x, 0, P3.y) + (new Vector3(P2.x - P3.x, 0.01f, P2.y - P3.y) / 2);
                Guide2.transform.localScale = new Vector3(Guide1.transform.lossyScale.x, 1, (P3 - P2).magnitude / 10);
                Guide2.transform.LookAt(new Vector3(P2.x, 0, P2.y));

                Mesh msh = PathMeshGenerator.Generate(new PathInfo(P1, P2, P3, new Spline(new Vector2(0, 0), new Vector2(1, 0), 0, 0)), 3f);
                var filt = RoadGuide.GetComponent<MeshFilter>();
                filt.mesh = msh;

                PointGuide.transform.position = new Vector3(P3.x, 0.01f, P3.y);

                break;
        }
    }
}