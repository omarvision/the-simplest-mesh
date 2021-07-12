using UnityEngine;
using System.Collections.Generic;

public class MakeMesh : MonoBehaviour
{
    private Mesh mesh = null;
    public List<Vector3> vertices = new List<Vector3>();
    public List<int> triangles = new List<int>();
    public List<Vector2> uv = new List<Vector2>();

    private void Start()
    {
        mesh = GetMesh();
        MakeTriangle(new Vector3(0,0,0));
        SetMesh();
    }
    public Mesh GetMesh()
    {
        MeshFilter meshfilter = this.GetComponent<MeshFilter>();
        Mesh mesh = null;
        if (Application.isEditor == true)
        {
            mesh = meshfilter.sharedMesh;
            if (mesh == null)
            {
                meshfilter.sharedMesh = new Mesh();
                mesh = meshfilter.sharedMesh;
            }
        }
        else
        {
            mesh = meshfilter.mesh;
            if (mesh == null)
            {
                meshfilter.mesh = new Mesh();
                mesh = meshfilter.mesh;
            }
        }
        return mesh;
    }
    public void Reset()
    {
        vertices.Clear();
        triangles.Clear();
        uv.Clear();
        SetMesh();
    }
    public void MakeTriangle(Vector3 position)
    {
        vertices.Add(new Vector3(0, 0, 0) + position);
        vertices.Add(new Vector3(0, 0, 1) + position);
        vertices.Add(new Vector3(1, 0, 0) + position);

        int firstVertex = vertices.Count - 3;

        triangles.Add(firstVertex);
        triangles.Add(firstVertex + 1);
        triangles.Add(firstVertex + 2);

        uv.Add(new Vector2(0, 0));
        uv.Add(new Vector2(0, 1));
        uv.Add(new Vector2(1, 0));
    }
    public void SetMesh()
    {
        if (mesh == null)
        {
            mesh = GetMesh();
        }

        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uv.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
    }
}
