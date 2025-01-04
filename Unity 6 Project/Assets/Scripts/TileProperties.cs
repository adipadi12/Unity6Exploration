using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TileProperties : MonoBehaviour
{
    private Renderer tileRenderer;
    private Material tileMaterial;
    
    private static TileProperties prevTile; // Tracks the previously selected tile
    private static TileProperties selectedTile; // Tracks the currently selected 

    [SerializeField] private AudioClip clickingSound;
    private float scale = 1.1f;
    private AudioSource audioSource;
    private void Awake()
    {
        tileRenderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void InitializeTile(Material material)
    {
        if (material != null)
        {
            tileMaterial = material;
            tileRenderer.material = material;
        }
        else
        {
            Debug.LogError("Material is null. Check your material list.");
        }
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
            prevTile.ResetMaterial();

            // A tile was previously selected, and now another tile is clicked
            selectedTile = this;
            audioSource.PlayOneShot(clickingSound);
            HighlightTile();

            // Check for adjacency and color match
            if (IsAdjacent(prevTile, selectedTile) &&
                prevTile.tileMaterial == selectedTile.tileMaterial)
            {
                DestroyTiles(prevTile, selectedTile);
            }

            // Update the previous tile to the current tile
            prevTile = selectedTile;
        }
    }


    private void HighlightTile()
    {
        tileRenderer.material.color = Color.black; // Highlight tile
    }

    private void ResetMaterial()
    {
        tileRenderer.material = tileMaterial; // Reset to the original color
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

        // Calculate differences in x and z
        float diffX = Mathf.Abs(pos1.x - pos2.x);
        float diffZ = Mathf.Abs(pos1.z - pos2.z);

        // Floating-point precision threshold
        float threshold = 0.05f;

        // Check for adjacency
        return (Mathf.Abs(diffX - scale) <= threshold && diffZ <= threshold) || // Horizontal neighbor
               (Mathf.Abs(diffZ - scale) <= threshold && diffX <= threshold) || // Vertical neighbor
               (Mathf.Abs(diffX - scale) <= threshold && Mathf.Abs(diffZ - scale) <= threshold); // Diagonal neighbor
    }

}
