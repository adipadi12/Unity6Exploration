using TMPro;
using UnityEngine;

public class TileProperties : MonoBehaviour
{
    private Renderer tileRenderer;
    private Color tileColor;
    
    private static TileProperties prevTile; // Tracks the previously selected tile
    private static TileProperties selectedTile; // Tracks the currently selected 

    [SerializeField] private AudioClip clickingSound;
    private AudioSource audioSource;
    private void Awake()
    {
        tileRenderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void InitializeTile(Color color)
    {
        tileColor = color;
        tileRenderer.material.color = color;
    }

    private void OnMouseDown()
    {
        SelectTile();
    }

    private void SelectTile()
    {
        if (prevTile == null)
        {
            // No tile previously selected, set this tile as the first selection
            prevTile = this;
            audioSource.PlayOneShot(clickingSound);
            HighlightTile();
        }
        else if (prevTile == this)
        {
            audioSource.PlayOneShot(clickingSound);
            // Clicking on the same tile, do nothing
            return;
        }
        else
        {
            // Reset the color of the previously selected tile before selecting a new one
            prevTile.ResetColor();

            // A tile was previously selected, and now another tile is clicked
            selectedTile = this;
            audioSource.PlayOneShot(clickingSound);
            HighlightTile();

            // Check for adjacency and color match
            if (IsAdjacent(prevTile, selectedTile) &&
                prevTile.tileColor == selectedTile.tileColor)
            {
                DestroyTiles(prevTile, selectedTile);
            }

            // Update the previous tile to the current tile
            prevTile = selectedTile;
        }
    }


    private void HighlightTile()
    {
        tileRenderer.material.color = Color.yellow; // Highlight tile
    }

    private void ResetColor()
    {
        tileRenderer.material.color = tileColor; // Reset to the original color
    }

    private void DestroyTiles(TileProperties tile1, TileProperties tile2)
    {
        ScoreManager.Instance.AddScore(1); //instance is using this function to update the score outside of this script
        //Debug.Log("Play destroy sound");
        Destroy(tile1.gameObject);
        Destroy(tile2.gameObject);
    }

    private bool IsAdjacent(TileProperties tile1, TileProperties tile2)
    {
        Vector3 pos1 = tile1.transform.position;
        Vector3 pos2 = tile2.transform.position;

        // Check adjacency (only horizontal, vertical or diagonal neighbors)
        return (Mathf.Abs(pos1.x - pos2.x) == 1f && pos1.z == pos2.z) || // Horizontal neighbor
               (Mathf.Abs(pos1.z - pos2.z) == 1f && pos1.x == pos2.x) || // Vertical neighbor
               (Mathf.Abs(pos1.x - pos2.x) == 1f && Mathf.Abs(pos1.z - pos2.z) == 1f);   //diagnol neighbour
    }
}
