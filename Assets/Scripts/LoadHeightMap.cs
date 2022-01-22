using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LoadHeightMap : MonoBehaviour
{

    private Terrain terrain;
    private TerrainData terrainData;

    [SerializeField]
    private Texture2D heightMapImage;

    [SerializeField]
    private Vector3 heightMapScale = new Vector3(1, 1, 1);

    [Header("PlayMode")]
    [SerializeField]
    private bool loadHieghtMap = true;

    [SerializeField]
    private bool flattenTerrainOnExit = true;

    [Header("EditorMode")]
    [SerializeField]
    private bool loadHieghtMapInEditMode = false;

    [SerializeField]
    private bool flattenTerrainInEditMode = false;

    // Start is called before the first frame update
    void Start()
    {

        if (terrain == null)
        {
            terrain = this.GetComponent<Terrain>();
        }

        if (terrainData == null)
        {
            terrainData = Terrain.activeTerrain.terrainData;
        }

        if (Application.IsPlaying(gameObject) && loadHieghtMap)
        {
            LoadHeightMapImage();
        }
    }

    void LoadHeightMapImage() 
    {


        //Width and the height
        float[,] heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int width = 0; width < terrainData.heightmapResolution; width++)
        {
            for(int height = 0; height < terrainData.heightmapResolution; height++)
            {
                heightMap[width,height] = heightMapImage.GetPixel((int)(width * heightMapScale.x), (int)(height * heightMapScale.z)).grayscale * 
                heightMapScale.y;
            }
        }

        terrainData.SetHeights(0,0, heightMap);
    }

    void FlatterTerrain()
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

    private void OnValidate()
    {

        if (terrain == null)
        {
            terrain = this.GetComponent<Terrain>();
        }

        if (terrainData == null)
        {
            terrainData = Terrain.activeTerrain.terrainData;
        }

        if (flattenTerrainInEditMode)
        {
            FlatterTerrain();
        }
        else if (loadHieghtMapInEditMode)
        {
            LoadHeightMapImage();
        }
    }

    private void OnDestroy()
    {
        if (flattenTerrainOnExit)
        {
            FlatterTerrain();
        }
    }
}
