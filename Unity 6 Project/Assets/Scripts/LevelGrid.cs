using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab; // Assign the Tile prefab in the Unity Inspector
    [SerializeField] private Transform gridParent; // Parent object to organize tiles
    [SerializeField] private Button restartButton;

    private GridSystem gridSystem;

    private void Awake()
    {
        gridSystem = new GridSystem(7, 7, 1f); // Create a 7x7 grid with a cell size of 1 unit
        gridSystem.CreateGridObjects(tilePrefab, gridParent); // Generate grid tiles
    }

    public void OnButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
