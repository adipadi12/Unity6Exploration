using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab; 
    [SerializeField] private Transform gridParent; 
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

        gridSystem = new GridSystem(7, 7, scale, materials); // a 7x7 grid with a cell size of scale
        gridSystem.CreateGridObjects(tilePrefab, gridParent); // grid tiles
    }

    public void OnButtonClick() //to reload scene on clicking button
    {
        SceneManager.LoadScene(0);
    }

    public Material GetRandomMaterial() //to enable new tiles being spawned to use random materials
    {
        return materials[Random.Range(0, materials.Count)];
    }
}
