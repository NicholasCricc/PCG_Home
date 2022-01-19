using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]


public class StartingPoint : MonoBehaviour
{

    [SerializeField]
    private int meshSize = 6;

    [SerializeField]
    private int submeshIndex = 0;

    [SerializeField]
    private Vector3 cubeSize = Vector3.one;

    // Start is called before the first frame update
    void Start()
    {
        RenderCube();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 CubeSize
    {
        get { return cubeSize; }
        set { cubeSize = value; }
    }

    private void RenderCube()
    {
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        MeshCollider meshCollider = this.GetComponent<MeshCollider>();

        meshFilter.mesh = CreateCube();
        meshCollider.sharedMesh = meshFilter.mesh;
        meshRenderer.materials = Materials.MaterialList().ToArray();
    }

    private Mesh CreateCube()
    {

        MeshGenerator meshGenerator = new MeshGenerator(meshSize);

        //top points
        Vector3 topPoint1 = new Vector3(cubeSize.x, cubeSize.y, -cubeSize.z);
        Vector3 topPoint2 = new Vector3(-cubeSize.x, cubeSize.y, -cubeSize.z);
        Vector3 topPoint3 = new Vector3(-cubeSize.x, cubeSize.y, cubeSize.z);
        Vector3 topPoint4 = new Vector3(cubeSize.x, cubeSize.y, cubeSize.z);

        //bottom points
        Vector3 bottomPoint1 = new Vector3(cubeSize.x, -cubeSize.y, -cubeSize.z);
        Vector3 bottomPoint2 = new Vector3(-cubeSize.x, -cubeSize.y, -cubeSize.z);
        Vector3 bottomPoint3 = new Vector3(-cubeSize.x, -cubeSize.y, cubeSize.z);
        Vector3 bottomPoint4 = new Vector3(cubeSize.x, -cubeSize.y, cubeSize.z);

        //top Square
        meshGenerator.CreateTriangle(topPoint1, topPoint2, topPoint3, 4);
        meshGenerator.CreateTriangle(topPoint1, topPoint3, topPoint4, 4);

        //bottom Square
        meshGenerator.CreateTriangle(bottomPoint3, bottomPoint2, bottomPoint1, 4);
        meshGenerator.CreateTriangle(bottomPoint4, bottomPoint3, bottomPoint1, 4);

        //front Square
        meshGenerator.CreateTriangle(bottomPoint1, bottomPoint2, topPoint2, 4);
        meshGenerator.CreateTriangle(bottomPoint1, topPoint2, topPoint1, 4);

        //right Side
        meshGenerator.CreateTriangle(bottomPoint4, topPoint1, topPoint4, 4);
        meshGenerator.CreateTriangle(bottomPoint4, bottomPoint1, topPoint1, 4);

        //left Side
        meshGenerator.CreateTriangle(bottomPoint2, topPoint3, topPoint2, 4);
        meshGenerator.CreateTriangle(bottomPoint2, bottomPoint3, topPoint3, 4);

        //back Sqaure
        meshGenerator.CreateTriangle(bottomPoint3, topPoint4, topPoint3, 4);
        meshGenerator.CreateTriangle(bottomPoint3, bottomPoint4, topPoint4, 4);



        return meshGenerator.CreateMesh();
    }



}
