using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Wall : MonoBehaviour
{

    [SerializeField]
    private int wallSize = 6;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject Test;

    [SerializeField]
    private GameObject StartCube;

    public float xPos;
    public float zPos;
    public int playerCount;

    public List<Vector3> FinishLocations = new List<Vector3>();

    private void Awake()
    {
        FinishLocations.Add(new Vector3 (1.62f, 0.62f, 0.87f));
        FinishLocations.Add(new Vector3(16.49f, 0.62f, 0.87f));
        FinishLocations.Add(new Vector3(16.91f, 0.62f, 16.33f));
        FinishLocations.Add(new Vector3(1.01f, 0.62f, 16.73f));
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateWall();
        PlayerSpawn();
        Finish();
    }

    public void Finish()
    {

        int Random_pos = Random.Range(0, 4);
        GameObject FinishingPoint = Instantiate(Test, FinishLocations[Random_pos], Quaternion.identity);

    }

    public void PlayerSpawn()
    {
        xPos = Random.Range(6.5f, 10f);
        zPos = Random.Range(6.5f, 9f);
        GameObject Starting_point = Instantiate(StartCube, new Vector3(xPos, 0.62f, zPos), Quaternion.identity);
        Instantiate(Player, Starting_point.transform.position, Quaternion.identity);
        

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void CreateWall()
    {

        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        MeshCollider meshCollider = this.GetComponent<MeshCollider>();

        Vector3 TopPosition = new Vector3(0f, 1f, -0.46f);
        Vector3 BottomPosition = new Vector3(0f, 1f, 18.528f);
        Vector3 RightPosition = new Vector3(-0.5f, 1f, 0f);
        Vector3 LeftPosition = new Vector3(18.5f, 1f, 0f);

        for (int i = 0; i < wallSize; i++)
        {


            GameObject TopBorder = InitialiseCube("Cube_Top " + i, TopPosition);
            TopPosition.x = TopPosition.x + TopBorder.GetComponent<Cube>().CubeSize.x * 2;
            TopBorder.GetComponent<Cube>().CubeSize = new Vector3(1f, 1f, 0.5f);
            meshCollider.sharedMesh = meshFilter.mesh;

            GameObject BottomBorder = InitialiseCube("Cube_Bottom " + i, BottomPosition);
            BottomPosition.x = BottomPosition.x + BottomBorder.GetComponent<Cube>().CubeSize.x * 2;
            BottomBorder.GetComponent<Cube>().CubeSize = new Vector3(1f, 1f, 0.5f);
            meshCollider.sharedMesh = meshFilter.mesh;

            GameObject RightBorder = InitialiseCube("Cube_Right " + i, RightPosition);
            RightPosition.z = RightPosition.z + RightBorder.GetComponent<Cube>().CubeSize.z * 2;
            RightBorder.GetComponent<Cube>().CubeSize = new Vector3(0.5f, 1f, 1f);
            meshCollider.sharedMesh = meshFilter.mesh;

            GameObject LeftBorder = InitialiseCube("Cube_Left " + i, LeftPosition);
            LeftPosition.z = LeftPosition.z + LeftBorder.GetComponent<Cube>().CubeSize.z * 2;
            LeftBorder.GetComponent<Cube>().CubeSize = new Vector3(0.5f, 1f, 1f);
            meshCollider.sharedMesh = meshFilter.mesh;
        }


        GameObject baseCube = InitialiseCube("base", new Vector3(9f, 0f, 9.03f));
        baseCube.GetComponent<Cube>().CubeSize = new Vector3(10, 0.12f, 10);
        

        //Maze
        GameObject Cube = InitialiseCube("cube0", new Vector3(1.07f, 0.5f, 2.39f));
        Cube.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);       

        GameObject Cube1 = InitialiseCube("cube1", new Vector3(3.07f, 0.5f, 2.39f));
        Cube1.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube3 = InitialiseCube("cube3", new Vector3(5.07f, 0.5f, 2.39f));
        Cube3.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube4 = InitialiseCube("cube4", new Vector3(8.57f, 0.5f, 2.39f));
        Cube4.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube5 = InitialiseCube("cube5", new Vector3(10.57f, 0.5f, 2.39f));
        Cube5.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube6 = InitialiseCube("cube6", new Vector3(14.07f, 0.5f, 2.39f));
        Cube6.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube7 = InitialiseCube("cube7", new Vector3(16.07f, 0.5f, 2.39f));
        Cube7.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f); //Horizontal

        GameObject Cube8 = InitialiseCube("cube8", new Vector3(9.57f, 0.5f, 1.14f));
        Cube8.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f); //vertical

        GameObject Cube9 = InitialiseCube("cube9", new Vector3(16.82f, 0.5f, 6.89f));
        Cube9.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube10 = InitialiseCube("cube10", new Vector3(15.57f, 0.5f, 3.64f));
        Cube10.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube11 = InitialiseCube("cube11", new Vector3(15.57f, 0.5f, 5.64f));
        Cube11.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube12 = InitialiseCube("cube12", new Vector3(15.57f, 0.5f, 7.64f));
        Cube12.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube13 = InitialiseCube("cube13", new Vector3(14.32f, 0.5f, 5.39f));
        Cube13.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube14 = InitialiseCube("cube14", new Vector3(13.57f, 0.5f, 6.64f));
        Cube14.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube15 = InitialiseCube("cube15", new Vector3(11.32f, 0.5f, 3.39f));
        Cube15.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube16 = InitialiseCube("cube16", new Vector3(11.32f, 0.5f, 5.39f));
        Cube16.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube17 = InitialiseCube("cube17", new Vector3(10.07f, 0.5f, 6.14f));
        Cube17.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube18 = InitialiseCube("cube18", new Vector3(11.32f, 0.5f, 7.39f));
        Cube18.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube19 = InitialiseCube("cube19", new Vector3(5.82f, 0.5f, 3.39f));
        Cube19.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube20 = InitialiseCube("cube20", new Vector3(5.82f, 0.5f, 5.39f));
        Cube20.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube21 = InitialiseCube("cube21", new Vector3(6.57f, 0.5f, 6.14f));
        Cube21.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube22 = InitialiseCube("cube22", new Vector3(4.57f, 0.5f, 6.14f));
        Cube22.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube23 = InitialiseCube("cube23", new Vector3(3.32f, 0.5f, 5.39f));
        Cube23.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube24 = InitialiseCube("cube24", new Vector3(3.32f, 0.5f, 4.39f));
        Cube24.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube25 = InitialiseCube("cube25", new Vector3(3.32f, 0.5f, 7.39f));
        Cube25.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube26 = InitialiseCube("cube26", new Vector3(5.82f, 0.5f, 7.39f));
        Cube26.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube27 = InitialiseCube("cube27", new Vector3(1.07f, 0.5f, 10.14f));
        Cube27.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube28 = InitialiseCube("cube28", new Vector3(3.07f, 0.5f, 10.14f));
        Cube28.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube29 = InitialiseCube("cube29", new Vector3(5.07f, 0.5f, 10.14f));
        Cube29.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube30 = InitialiseCube("cube30", new Vector3(5.82f, 0.5f, 11.39f));
        Cube30.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube31 = InitialiseCube("cube31", new Vector3(10.07f, 0.5f, 10.14f));
        Cube31.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube32 = InitialiseCube("cube32", new Vector3(8.57f, 0.5f, 10.14f));
        Cube32.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube33 = InitialiseCube("cube33", new Vector3(11.32f, 0.5f, 10.89f));
        Cube33.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube34 = InitialiseCube("cube34", new Vector3(12.57f, 0.5f, 10.14f));
        Cube34.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube35 = InitialiseCube("cube35", new Vector3(16.82f, 0.5f, 10.14f));
        Cube35.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube36 = InitialiseCube("cube36", new Vector3(13.32f, 0.5f, 10.89f));
        Cube36.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube37 = InitialiseCube("cube37", new Vector3(16.07f, 0.5f, 11.39f));
        Cube37.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube38 = InitialiseCube("cube38", new Vector3(2.57f, 0.5f, 4.89f));
        Cube38.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube39 = InitialiseCube("cube39", new Vector3(2.07f, 0.5f, 11.39f));
        Cube39.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube40 = InitialiseCube("cube40", new Vector3(2.07f, 0.5f, 13.39f));
        Cube40.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube41 = InitialiseCube("cube41", new Vector3(15.32f, 0.5f, 12.39f));
        Cube41.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube42 = InitialiseCube("cube42", new Vector3(11.32f, 0.5f, 12.89f));
        Cube42.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube43 = InitialiseCube("cube43", new Vector3(11.32f, 0.5f, 14.89f));
        Cube43.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube44 = InitialiseCube("cube44", new Vector3(10.07f, 0.5f, 15.64f));
        Cube44.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube45 = InitialiseCube("cube45", new Vector3(5.07f, 0.5f, 12.64f));
        Cube45.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube46 = InitialiseCube("cube46", new Vector3(7.82f, 0.5f, 11.39f));
        Cube46.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube47 = InitialiseCube("cube47", new Vector3(7.82f, 0.5f, 13.39f));
        Cube47.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube48 = InitialiseCube("cube48", new Vector3(13.32f, 0.5f, 14.64f));
        Cube48.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube49 = InitialiseCube("cube49", new Vector3(16.32f, 0.5f, 14.64f));
        Cube49.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube50 = InitialiseCube("cube50", new Vector3(15.32f, 0.5f, 15.64f));
        Cube50.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube51 = InitialiseCube("cube51", new Vector3(4.57f, 0.5f, 15.64f));
        Cube51.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube52 = InitialiseCube("cube52", new Vector3(2.07f, 0.5f, 16.89f));
        Cube52.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube53 = InitialiseCube("cube53", new Vector3(7.32f, 0.5f, 16.64f));
        Cube53.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);

        GameObject Cube54 = InitialiseCube("cube54", new Vector3(10.07f, 0.5f, 12.39f));
        Cube54.GetComponent<Cube>().CubeSize = new Vector3(1f, 0.5f, 0.2f);

        GameObject Cube55 = InitialiseCube("cube55", new Vector3(5.32f, 0.5f, 16.89f));
        Cube55.GetComponent<Cube>().CubeSize = new Vector3(0.2f, 0.5f, 1f);
    }

    private GameObject InitialiseCube(string name, Vector3 nextPosition)
    {
        MeshCollider meshCollider = this.GetComponent<MeshCollider>();
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();

        GameObject cube = new GameObject();
        cube.name = name;
        Cube cubeScript = cube.AddComponent<Cube>();
        cube.transform.position = nextPosition;
        cube.transform.parent = this.transform;
        meshCollider.sharedMesh = meshFilter.mesh;
        return cube;
    }
}
