using System;
using UnityEngine;

[Serializable]
public class MapData
{
    public int width;
    public int height;
    public float[] heights;

    public void Init(Terrain terrain)
    {
        //terrain은 정사각형
        TerrainData td = terrain.terrainData;
        width = td.heightmapResolution;
        height = td.heightmapResolution;

        float[,] heights2D = new float[width, height];
        float[] heights1D = new float[width * height];

        int index = 0;

        for (int i = 0; i < heights2D.GetLength(0); i++)
        {
            for(int j = 0;  j < heights2D.GetLength(1); j++)
            {
                heights2D[i, j] = 1f;
                heights1D[index++] = heights2D[i, j];
            }
        }

        td.SetHeights(0, 0, heights2D);
    }

  
    public void LoadTerrain(Terrain terrain)
    {
        TerrainData td = terrain.terrainData;
        width = td.heightmapResolution;
        height = td.heightmapResolution;

        float[,] heights2D = new float[width, height];
        float[] heights1D = new float[width * height];

        int index = 0;

        for (int i = 0; i < heights2D.GetLength(0); i++)
        {
            for (int j = 0; j < heights2D.GetLength(1); j++)
            {
                heights2D[i, j] = heights[index++];
            }
        }

        td.SetHeights(0, 0, heights2D);
    }

    // Terrain → Data
    public void SaveFromTerrain(Terrain terrain)
    {
        TerrainData td = terrain.terrainData;

        width = td.heightmapResolution;
        height = td.heightmapResolution;

        float[,] heights2D = td.GetHeights(0, 0, width, height);
        heights = new float[width * height];

        int index = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[index++] = heights2D[x, y];
            }
        }
    }

}