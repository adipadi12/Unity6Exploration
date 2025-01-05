using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;
    private float cellSize;
    private List<Material> materials;
    public GridSystem(int width, int height, float cellSize, List<Material> materials)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.materials = materials;
    }

    //grid coordinates to world position
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize;
    }

    // converts world position to grid coordinates
    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x / cellSize);
        int z = Mathf.RoundToInt(worldPosition.z / cellSize);
        return new Vector2Int(x, z);
    }

    //the tiles and places them in the scene
    public void CreateGridObjects(Transform tilePrefab, Transform parent)
    {
        Quaternion tileRotation = tilePrefab.rotation; //to instantiate the tiles in their rotated form
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 worldPosition = GetWorldPosition(x, z);
                Transform tileTransform = GameObject.Instantiate(tilePrefab, worldPosition, tileRotation, parent);
                TileProperties tile = tileTransform.GetComponent<TileProperties>();
                if (tile != null)
                {
                    tile.InitializeTile(RandomMaterial());
                }
            }
        }
    }

    //a random color for tiles
    private Material RandomMaterial()
    {
        if (materials != null && materials.Count > 0)
        {
            return materials[Random.Range(0, materials.Count)];
        }
        Debug.LogError("Material list is empty. Assign materials in the LevelGrid script.");
        return null;
    }

}
