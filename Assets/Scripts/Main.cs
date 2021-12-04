using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Mesh m;

    Material mat;
    List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        mat = new Material(Shader.Find("Unlit/Point"));
        mat.SetColor("_BaseColor", Color.cyan);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            DepthCastHit hit;
            if (DepthCast.Cast(Input.mousePosition, out hit))
                positions.Add(hit.position);
        }

        for (int i = 0; i < positions.Count; i++)
        {
            Graphics.DrawMesh(m, Matrix4x4.TRS(positions[i], Quaternion.identity, Vector3.one * 0.1f), mat, LayerMask.GetMask("Default"));
        }
    }
}