using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerrainTextureData
{
    public Texture2D terrainTexture;
    public Vector3 tileSize;

    public float minHeight;

    public float maxHeight;
}

[System.Serializable]
public class TreeData
{
    public GameObject treeMesh;
    public float minHeight;
    public float maxHeight;
}

[System.Serializable]
public class VegData
{
    public GameObject vegMesh;
    public float minHeight;
    public float maxHeight;
}


public class GenerateRandomHeights : MonoBehaviour
{
    private Terrain terrain;
    private TerrainData terrainData;

    [SerializeField]
    [Range(0f, 1f)]
    private float minRandomHeightRange = 0f; //min is 0;

    [SerializeField]
    [Range(0f, 1f)]
    private float maxRandomHeightRange = 0.1f; //max is 1

    [SerializeField]
    private bool flattenTerrain = true;

    [Header("Perlin Noise")]
    [SerializeField]
    private bool perlinNoise = false;

    [SerializeField]
    private float perlinNoiseWidthScale = 0.01f;

    [SerializeField]
    private float perlinNoiseHeightScale = 0.01f;

    [Header("Texture Data")]
    [SerializeField]
    private List<TerrainTextureData> terrainTextureData;

    [SerializeField]
    private bool addTerrainTexture = false;

    [SerializeField]
    private float terrainTextureBlendOffset = 0.01f;

    [Header("Tree Data")]
    [SerializeField]
    private List<TreeData> treeData;

    [SerializeField]
    private int maxTrees = 2000;

    [SerializeField]
    private int treeSpacing = 10;

    [SerializeField]
    private bool addTrees = false;

    [SerializeField]
    private int terrainLayerIndex;

    [Header("Vegetation")]
    [SerializeField]
    private List<VegData> vegData;

    [SerializeField]
    private int maxVeg = 2000;

    [SerializeField]
    private int vegSpacing = 10;

    [SerializeField]
    private bool addVeg = false;

    [SerializeField]
    private int terrainLayerIndexVeg;

    [Header("Water")]
    [SerializeField]
    private GameObject water;

    [SerializeField]
    private float waterHeight = 0.3f;

    [Header("Cloud")]
    [SerializeField]
    private GameObject cloud;
    public float CminHeight;
    public float CmaxHeight;

    [SerializeField]
    private float cloudHeight = 0.3f;

    [Header("Fog")]
    [SerializeField]
    private GameObject fog;

    [SerializeField]
    private float fogHeight = 0.3f;

    [Header("Player")]
    public float xPos;
    public float zPos;
    public int playerCount;

    [SerializeField]
    private GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        if(terrain == null)
        {
            terrain = this.GetComponent<Terrain>();
        }

        if(terrainData == null)
        {
            terrainData = Terrain.activeTerrain.terrainData;
        }

        GenerateHeights();

        AddTerrainTexture();

        AddTrees();

        //AddVegetation();

        AddWater();

        AddClouds();

        AddFog();

        PlayerSpawn();
    }

    private void GenerateHeights()
    {
        float[,] heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for(int width = 0; width < terrainData.heightmapResolution; width++)
        {
            for(int height = 0; height < terrainData.heightmapResolution; height++)
            {
                
                if (perlinNoise)
                {
                    heightMap[width, height] = Mathf.PerlinNoise(width * perlinNoiseWidthScale, height * perlinNoiseHeightScale);
                }
                else
                {
                    heightMap[width, height] = Random.Range(minRandomHeightRange, maxRandomHeightRange);
                }

                /**
                 
                 heightMap[width, height] = Random.Range(minRandomHeightRange, maxRandomHeightRange);
                 heightMap[width, height] = Mathf.PerlinNoise(width * perlinNoiseWidthScale, height * perlinNoiseHeightScale);

                **/


            }
        }

        terrainData.SetHeights(0, 0, heightMap);
    }

    private void FlattenTerrain()
    {
        float[,] heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int width = 0; width < terrainData.heightmapResolution; width++)
        {
            for (int height = 0; height < terrainData.heightmapResolution; height++)
            {
                heightMap[width, height] = 0;
            }
        }

        terrainData.SetHeights(0, 0, heightMap);
    }

    private void AddTerrainTexture()
    {
        TerrainLayer[] terrainLayers = new TerrainLayer[terrainTextureData.Count];

        for (int i =0; i < terrainTextureData.Count; i++)
        {
            if (addTerrainTexture)
            {
                terrainLayers[i] = new TerrainLayer();
                terrainLayers[i].diffuseTexture = terrainTextureData[i].terrainTexture;
                terrainLayers[i].tileSize = terrainTextureData[i].tileSize;
            }
            else
            {
                terrainLayers[i] = new TerrainLayer();
                terrainLayers[i].diffuseTexture = null;
            }
        }

        terrainData.terrainLayers = terrainLayers;

        float[,] heightMap = terrainData.GetHeights(0,0,terrainData.heightmapResolution, terrainData.heightmapResolution);

        float[, ,] alphamapList = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

        for(int height = 0; height < terrainData.alphamapHeight; height++)
        {
            for(int width = 0; width < terrainData.alphamapWidth; width++)
            {
                float[] alphamap = new float[terrainData.alphamapLayers];

                for(int i = 0; i < terrainTextureData.Count; i++)
                {
                    float heightBegin = terrainTextureData[i].minHeight - terrainTextureBlendOffset;
                    float heightEnd = terrainTextureData[i].maxHeight + terrainTextureBlendOffset;

                    if(heightMap[width, height] >= heightBegin && heightMap[width,height] <= heightEnd)
                    {
                        alphamap[i] = 1;
                    }
                }

                Blend(alphamap);

                for(int j =0; j < terrainTextureData.Count; j++)
                {
                    alphamapList[width, height, j] = alphamap[j];
                }
            }
        }

        terrainData.SetAlphamaps(0, 0, alphamapList);
    }

    private void Blend(float[] alphamap)
    {
        float total = 0;

        for(int i = 0; i < alphamap.Length; i++)
        {
            total += alphamap[i];
        }

        for(int i = 0; i < alphamap.Length; i++)
        {
            alphamap[i] = alphamap[i] / total;
        }
    }

    private void AddTrees()
    {
        TreePrototype[] trees = new TreePrototype[treeData.Count];

        for(int i = 0; i < treeData.Count; i++)
        {
            trees[i] = new TreePrototype();
            trees[i].prefab = treeData[i].treeMesh;
        }

        terrainData.treePrototypes = trees;

        List<TreeInstance> treeInstanceList = new List<TreeInstance>();

        if (addTrees)
        {
            for(int z = 0; z < terrainData.size.z; z+= treeSpacing)
            {
                for(int x = 0; x < terrainData.size.x; x += treeSpacing)
                {
                    for(int treeIndex = 0; treeIndex < trees.Length; treeIndex++)
                    {
                        if (treeInstanceList.Count < maxTrees)
                        {
                            float currentHeight = terrainData.GetHeight(x, z) / terrainData.size.y; // this is going to give us a height value between 0 and 1

                            if(currentHeight >= treeData[treeIndex].minHeight && currentHeight <= treeData[treeIndex].maxHeight)
                            {
                                float randomX = (x + Random.Range(-5.0f, 5.0f)) / terrainData.size.x;

                                float randomZ = (z + Random.Range(-5.0f, 5.0f)) / terrainData.size.z;

                                Vector3 treePosition = new Vector3(randomX * terrainData.size.x, 
                                                                          currentHeight * terrainData.size.y, 
                                                                          randomZ * terrainData.size.z) + this.transform.position;
                                RaycastHit raycastHit;

                                int layerMask = 1 << terrainLayerIndex;

                                if (Physics.Raycast(treePosition, -Vector3.up, out raycastHit, 100, layerMask) || 
                                    Physics.Raycast(treePosition, Vector3.up, out raycastHit, 100, layerMask))
                                {
                                    float treeDistance = (raycastHit.point.y - this.transform.position.y) / terrainData.size.y;

                                    TreeInstance treeInstance = new TreeInstance();

                                    treeInstance.position = new Vector3(randomX, treeDistance, randomZ);
                                    treeInstance.rotation = Random.Range(0, 360);
                                    treeInstance.prototypeIndex = treeIndex;
                                    treeInstance.color = Color.white;
                                    treeInstance.lightmapColor = Color.white;
                                    treeInstance.heightScale = 0.95f;
                                    treeInstance.widthScale = 0.95f;

                                    treeInstanceList.Add(treeInstance);
                                }


                            }
                        }
                    }
                }
            }
        }

        terrainData.treeInstances = treeInstanceList.ToArray();

    }

    //private void AddVegetation()
    //{
    //    TreePrototype[] Vegs = new TreePrototype[vegData.Count];

    //    for (int i = 0; i < vegData.Count; i++)
    //    {
    //        Vegs[i] = new TreePrototype();
    //        Vegs[i].prefab = vegData[i].vegMesh;
    //    }

    //    terrainData.treePrototypes = Vegs;

    //    List<TreeInstance> vegInstanceList = new List<TreeInstance>();

    //    if (addTrees)
    //    {
    //        for (int z = 0; z < terrainData.size.z; z += vegSpacing)
    //        {
    //            for (int x = 0; x < terrainData.size.x; x += vegSpacing)
    //            {
    //                for (int vegIndex = 0; vegIndex < Vegs.Length; vegIndex++)
    //                {
    //                    if (vegInstanceList.Count < maxTrees)
    //                    {
    //                        float currentHeight = terrainData.GetHeight(x, z) / terrainData.size.y; // this is going to give us a height value between 0 and 1

    //                        if (currentHeight >= vegData[vegIndex].minHeight && currentHeight <= vegData[vegIndex].maxHeight)
    //                        {
    //                            float randomX = (x + Random.Range(-5.0f, 5.0f)) / terrainData.size.x;

    //                            float randomZ = (z + Random.Range(-5.0f, 5.0f)) / terrainData.size.z;

    //                            Vector3 vegPosition = new Vector3(randomX * terrainData.size.x,
    //                                                                      currentHeight * terrainData.size.y,
    //                                                                      randomZ * terrainData.size.z) + this.transform.position;
    //                            RaycastHit raycastHit;

    //                            int layerMask = 1 << terrainLayerIndex;

    //                            if (Physics.Raycast(vegPosition, -Vector3.up, out raycastHit, 100, layerMask) ||
    //                                Physics.Raycast(vegPosition, Vector3.up, out raycastHit, 100, layerMask))
    //                            {
    //                                float vegDistance = (raycastHit.point.y - this.transform.position.y) / terrainData.size.y;

    //                                TreeInstance vegInstance = new TreeInstance();

    //                                vegInstance.position = new Vector3(randomX, vegDistance, randomZ);
    //                                vegInstance.rotation = Random.Range(0, 360);
    //                                vegInstance.prototypeIndex = vegIndex;
    //                                vegInstance.color = Color.white;
    //                                vegInstance.lightmapColor = Color.white;
    //                                vegInstance.heightScale = 0.95f;
    //                                vegInstance.widthScale = 0.95f;

    //                                vegInstanceList.Add(vegInstance);
    //                            }


    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    terrainData.treeInstances = vegInstanceList.ToArray();

    //}

    private void AddWater()
    {
        GameObject waterGameObject = Instantiate(water, this.transform.position, this.transform.rotation);
        waterGameObject.name = "Water";
        int RandX = (int)Random.Range(0, terrainData.size.x / 2);
        int RandY = (int)Random.Range(0, waterHeight * terrainData.size.y);
        int RandZ = (int)Random.Range(0, terrainData.size.z / 2);
        waterGameObject.transform.position = this.transform.position + new Vector3(RandX, RandY,
        RandZ);
        waterGameObject.transform.localScale = new Vector3(terrainData.size.x, 1, terrainData.size.z);
    }

    private void AddClouds()
    {
        GameObject cloudGameObject = Instantiate(cloud, this.transform.position, this.transform.rotation);
        cloudGameObject.name = "Cloud";
        int RandX = (int)Random.Range(0, terrainData.size.x / 2);
        int RandY = (int)Random.Range(0, cloudHeight * terrainData.size.y);
        int RandZ = (int)Random.Range(0, terrainData.size.z / 2);
        cloudGameObject.transform.position = this.transform.position + new Vector3(RandX, RandY,
        RandZ);
        cloudGameObject.transform.localScale = new Vector3(terrainData.size.x, 1, terrainData.size.z);
    }    
    private void AddFog()
    {
        GameObject fogGameObject = Instantiate(fog, this.transform.position, this.transform.rotation);
        fogGameObject.name = "Fog";
        int RandX = (int)Random.Range(0, terrainData.size.x / 2);
        int RandY = (int)Random.Range(0, fogHeight * terrainData.size.y);
        int RandZ = (int)Random.Range(0, terrainData.size.z / 2);
        fogGameObject.transform.position = this.transform.position + new Vector3(RandX, RandY,
        RandZ);
        fogGameObject.transform.localScale = new Vector3(terrainData.size.x, 1, terrainData.size.z);
    }

    public void PlayerSpawn()
    {
        xPos = Random.Range(117, 160f);
        zPos = Random.Range(137f, 237f);
        Instantiate(Player, new Vector3(xPos, 441.1f, zPos), Quaternion.identity);

    }
    private void OnDestroy()
    {
        if (flattenTerrain)
        {
            FlattenTerrain();
        }
    }

}
