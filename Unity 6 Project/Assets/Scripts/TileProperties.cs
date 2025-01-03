using UnityEngine;

public class TileProperties : MonoBehaviour
{
    private Renderer tileRenderer;
    private Color tileColor;

    private void Awake()
    {
        tileRenderer = GetComponent<Renderer>();
    }

    public void InitializeTile(Color color)
    {
        tileColor = color;
        tileRenderer.material.color = color;
    }

    private void OnMouseDown()
    {
       // Debug.Log($"Tile clicked: {tileColor}");
        HighlightTile();
    }

    private void HighlightTile()
    {
        tileRenderer.material.color = Color.yellow; // Highlight tile
        Invoke(nameof(ResetColor), 0.2f); // Reset color after 0.2 seconds
    }

    private void ResetColor()
    {
        tileRenderer.material.color = tileColor;
    }
}
