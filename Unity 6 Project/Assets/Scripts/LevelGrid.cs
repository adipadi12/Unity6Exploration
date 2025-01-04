using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab; // Assign the Tile prefab in the Unity Inspector
    [SerializeField] private Transform gridParent; // Parent object to organize tiles
    //[SerializeField] private Button restartButton;
    [SerializeField] private List<Material> materials;
    [SerializeField] private float scale = 1.1f;

    private GridSystem gridSystem;

    private void Awake()
    {
        if (materials == null || materials.Count == 0)
        {
            Debug.LogError("Please assign materials in the Inspector.");
            return;
        }

        gridSystem = new GridSystem(7, 7, scale, materials); // Create a 7x7 grid with a cell size of 1 unit
        gridSystem.CreateGridObjects(tilePrefab, gridParent); // Generate grid tiles
    }

    public void OnButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
