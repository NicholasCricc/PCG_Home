using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator
{
    private List<Vector3> pointsList = new List<Vector3>(); //store a list of points

    private List<int> triangleList = new List<int>(); // defines triangles by storing indices to the points List that make up a triangle

    private List<Vector3> normalsList = new List<Vector3>(); //store the normals - direction of the faces

    private List<int>[] submeshList = new List<int>[]{}; //stores the submeshes by storing indices to the points list

    private List<Vector2> uvList = new List<Vector2>();

    //Constructor
    public MeshGenerator(int submeshCount){
        submeshList = new List<int>[submeshCount];

        for(int i=0; i < submeshList.Length; i++){
            submeshList[i] = new List<int>();
        }
    }

    public void CreateTriangle(Vector3 point1, Vector3 point2, Vector3 point3, int submeshIndex)
    {

        //calculate the normal
        Vector3 normal = Vector3.Cross(point2 - point1, point3 - point1).normalized;

        //getting the next available indexes in our points list
        int point1Index = pointsList.Count;
        int point2Index = pointsList.Count + 1;
        int point3Index = pointsList.Count + 2;

        //adding the points
        pointsList.Add(point1);
        pointsList.Add(point2);
        pointsList.Add(point3);


        //adding points to the triangle
        triangleList.Add(point1Index);
        triangleList.Add(point2Index);
        triangleList.Add(point3Index);

        // adding those points to a specifi submesh
        submeshList[submeshIndex].Add(point1Index);
        submeshList[submeshIndex].Add(point2Index);
        submeshList[submeshIndex].Add(point3Index);

        // adding normals to each point
        normalsList.Add(normal);
        normalsList.Add(normal);
        normalsList.Add(normal);

        //adding u,v coordinates to each point
        uvList.Add(new Vector2(0, 0));
        uvList.Add(new Vector2(0, 1));
        uvList.Add(new Vector2(1, 1));
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();

        mesh.vertices = pointsList.ToArray();
        mesh.triangles = triangleList.ToArray();
        mesh.normals = normalsList.ToArray();
        mesh.uv = uvList.ToArray();

        mesh.subMeshCount = submeshList.Length;

        for(int i = 0; i < submeshList.Length; i++)
        {

            if(submeshList[i].Count < 3)
            {
                mesh.SetTriangles(new int[3] { 0, 0, 0, },i);
            }
            else
            {
                mesh.SetTriangles(submeshList[i].ToArray(), i);
            }

            
        }

        return mesh;

    }
}