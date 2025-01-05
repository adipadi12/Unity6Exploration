using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } //singleton instance
    public TextMeshProUGUI scoreText; //reference to Text

    [SerializeField] private AudioClip destroySound; //assign this in the Inspector
    private AudioSource audioSource;

    private int score = 0;

    [SerializeField] private GameObject tilePrefab; //referencee to tile

    [SerializeField]private LevelGrid levelGrid; //reference to LevelGrid script
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this; //referencing the instance and checking for if references are null
        }
        audioSource = GetComponent<AudioSource>(); //using audio source attached to score manager to play destroy sound
        //not the most efficient method but it's working for now
    }

    public void AddScore(int points) //function to add score
    {
        score += points;
        audioSource.PlayOneShot(destroySound);
        scoreText.text = score.ToString(); //converting text to string so it can be used by canvas
    }

    public void InstantiateAndMoveTile(Vector3 position1, Vector3 position2, Quaternion rotation)
    {
        StartCoroutine(InstantiateAndMoveCoroutine(position1, position2, rotation)); //instantiating new tile after old tiles destroyed
        Debug.Log("New tile spawned");
    }

    private IEnumerator InstantiateAndMoveCoroutine(Vector3 position1, Vector3 position2, Quaternion rotation)
    {
        yield return new WaitForSeconds(1f); //used coroutine here because i wanted tiles to spawn after a second

        GameObject newTile = Instantiate(tilePrefab, transform.position, rotation);

        Material randomMaterial = levelGrid.GetRandomMaterial();
        TileProperties tileProperties = newTile.GetComponent<TileProperties>();
        tileProperties.InitializeTile(randomMaterial); //using the function in the tileprops script to get the tile with a random color

        Vector3 midpoint = (position1 + position2) / 2; //this was getting the tile in between the 2 positions but das cringe lol

        float duration = 0.5f;
        float elapsedTime = 0f;
        Vector3 startPosition = newTile.transform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            newTile.transform.position = Vector3.Lerp(startPosition, midpoint, elapsedTime / duration); //interpolating for smooth translation animation of tile falling 
            yield return null;
        }

        newTile.transform.position = midpoint; //falling at midpoint but sorted this out in tileproperties
    }
}