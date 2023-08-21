using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private Transform player;
    public GameObject[] tilePrefabs;
    private List<GameObject> activeTiles = new List<GameObject>();

    private float spawnPos = 0;
    private float tileLenght = 100;
    private int startTiles = 6;

    public void Initialize()
    {
        for (int i = 0; i < startTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
            }
            else if (i == 1)
            {
                SpawnTile(1);
            }
            else
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));
            }
        }
    }

    private void Update()
    {
        if (player.position.z - 60 > spawnPos - (startTiles * tileLenght))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject tile = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPos, transform.rotation);
        activeTiles.Add(tile);
        spawnPos += tileLenght;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
