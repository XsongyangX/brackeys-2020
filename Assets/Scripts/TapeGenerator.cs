using UnityEngine;

/// <summary>
/// Generates all the tapes at Start
/// </summary>
public class TapeGenerator : MonoBehaviour
{
    // Stores all the possible tape variants
    [SerializeField] private GameObject[] tapePrefabs = default;
    // The minimum number of tapes spawned in the entire scene
    [SerializeField] private int minTapes = default;
    // All the references to all the shelves in the scene
    private ShelfManager[] shelfManagers;

    private void Start()
    {
        GetAllShelves();
        GenerateTapes();
    }

    /// <summary>
    /// Gets all the shelves in the scene and populates the shelfManagers array
    /// </summary>
    private void GetAllShelves()
    {
        // Get all the shelves GameObjects
        GameObject[] shelves = GameObject.FindGameObjectsWithTag("Shelf");

        shelfManagers = new ShelfManager[shelves.Length];

        // Populate the shelfManagers array with all the ShelfManager components
        for (int i = 0; i < shelves.Length; ++i)
        {
            shelfManagers[i] = shelves[i].GetComponent<ShelfManager>();
        }
    }

    /// <summary>
    /// Generates some random tapes and places them in random spawn points across the entire scene on the shelves
    /// </summary>
    private void GenerateTapes()
    {
        // Counts the available spawn points in the scene
        int availableSpawnPoints = 0;
        // Counts how many tapes we have to spawn at least
        int min = minTapes;

        // Find the number of total spawn points
        for (int i = 0; i < shelfManagers.Length; ++i)
        {
            availableSpawnPoints += shelfManagers[i].TapeSpawnPoints.Length;
        }

        // Iterate over each shelf and each spawn point
        for (int i = 0; i < shelfManagers.Length; ++i)
        {
            for (int j = 0; j < shelfManagers[i].TapeSpawnPoints.Length; ++j)
            {
                // Generate a random tapeVariant
                int tapeVariant = Random.Range(0, tapePrefabs.Length);

                // If we can still randomize the spawn
                if (availableSpawnPoints > min)
                {
                    // 0 = don't spawn, 1 = spawn
                    int r = Random.Range(0, 2);

                    if (r == 1)
                    {
                        Vector3 position = new Vector3(shelfManagers[i].TapeSpawnPoints[j].position.x, shelfManagers[i].TapeSpawnPoints[j].position.y + tapePrefabs[tapeVariant].transform.localScale.y / 2f, shelfManagers[i].TapeSpawnPoints[j].position.z);
                        Instantiate(tapePrefabs[tapeVariant], position, shelfManagers[i].transform.rotation, shelfManagers[i].transform);
                        --min;
                    }
                }
                else
                {
                    Vector3 position = new Vector3(shelfManagers[i].TapeSpawnPoints[j].position.x, shelfManagers[i].TapeSpawnPoints[j].position.y + tapePrefabs[tapeVariant].transform.localScale.y / 2f, shelfManagers[i].TapeSpawnPoints[j].position.z);
                    Instantiate(tapePrefabs[tapeVariant], position, shelfManagers[i].transform.rotation, shelfManagers[i].transform);
                    --min;
                }
                
                --availableSpawnPoints;
            }
        }
    }
}
