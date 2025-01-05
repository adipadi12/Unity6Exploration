using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TileProperties : MonoBehaviour
{
    private Renderer tileRenderer;
    private Material tileMaterial;
    
    private static TileProperties prevTile; //  the previously selected tile
    private static TileProperties selectedTile; //  the currently selected 

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
            tileRenderer.material = material; //calling the renderer of the tile in this function to change its material
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
            // no tile previously selected so set this tile as the first selection
            prevTile = this;
            audioSource.PlayOneShot(clickingSound);
            HighlightTile();
        }
        else if (prevTile == this)
        {
            audioSource.PlayOneShot(clickingSound);
            // clicking on the same tile do nothing
            return;
        }
        else
        {
            // reset the color of the previously selected tile before selecting a new one
            prevTile.ResetMaterial();

            // tile was previously selected now another tile is clicked
            selectedTile = this;
            audioSource.PlayOneShot(clickingSound);
            HighlightTile();

            //for adjacency and color match
            if (IsAdjacent(prevTile, selectedTile) &&
                prevTile.tileMaterial == selectedTile.tileMaterial)
            { 
                DestroyTiles(prevTile, selectedTile);
            }

            // update the previous tile to the current tile
            prevTile = selectedTile;
        }
    }


    private void HighlightTile()
    {
        tileRenderer.material.color = Color.black; // highlight tile
    }

    private void ResetMaterial()
    {
        tileRenderer.material = tileMaterial; // reset to the original color
    }

    private void DestroyTiles(TileProperties tile1, TileProperties tile2)
    {
        ScoreManager.Instance.AddScore(1); //instance is using this function to update the score outside of this script
        //Debug.Log("Play destroy sound");
        Destroy(tile1.gameObject);
        Destroy(tile2.gameObject);

        Vector3 pos1 = tile1.transform.position;
        Vector3 pos2 = tile2.transform.position;

        ScoreManager.Instance.InstantiateAndMoveTile(pos1, pos1, tile1.transform.rotation); //used the coroutine + function made in spawn manager to 
        //spawn new tile after destruction of 2. passed the 2 parameters as the same so it doesn't spawn at the midpoint. also gave them the rotation 
        //values of the original tile which was causing issues
    }

    private bool IsAdjacent(TileProperties tile1, TileProperties tile2)
    {
        Vector3 pos1 = tile1.transform.position;
        Vector3 pos2 = tile2.transform.position;

        // calculate differences in x and z
        float diffX = Mathf.Abs(pos1.x - pos2.x);
        float diffZ = Mathf.Abs(pos1.z - pos2.z);

        // floating-point precision threshold
        float threshold = 0.05f;

        // check for adjacency
        return (Mathf.Abs(diffX - scale) <= threshold && diffZ <= threshold) || // horizontal neighbor
               (Mathf.Abs(diffZ - scale) <= threshold && diffX <= threshold) || // vertical neighbor
               (Mathf.Abs(diffX - scale) <= threshold && Mathf.Abs(diffZ - scale) <= threshold); // diagonal neighbor

    }

}
