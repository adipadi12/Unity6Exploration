using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;
    private float cellSize;

    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
    }

    // Converts grid coordinates to world position
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize;
    }

    // Converts world position to grid coordinates
    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x / cellSize);
        int z = Mathf.RoundToInt(worldPosition.z / cellSize);
        return new Vector2Int(x, z);
    }

    // Creates the tiles and places them in the scene
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
                    tile.InitializeTile(RandomColor());
                }
            }
        }
    }

    // Generates a random color for tiles
    private Color RandomColor()
    {
        Color[] colors = { Color.red, Color.green, Color.blue, Color.clear};
        return colors[Random.Range(0, colors.Length)];
    }
}
