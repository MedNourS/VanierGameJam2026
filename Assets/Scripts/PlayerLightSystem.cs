using System;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerLightSystem : MonoBehaviour
{
    [SerializeField] private Tilemap photons;
    [SerializeField] private Tilemap obstacles;
    [SerializeField] private TileBase photonTile;
    [SerializeField] private TileBase litLanternTile;
    [SerializeField] private LightRotation lightRotation;
    [SerializeField] private Tilemap lanternsTileMap;
    [SerializeField] private TextMeshProUGUI textMesh;
    private TilemapCollider2D photonsCollider;
    private Vector3Int nextTile;
    private int lanternCount = 0;
    public int photonsLeft;
    void Start()
    {
        photonsCollider = photons.GetComponent<TilemapCollider2D>();
        Vector3Int pos = photons.WorldToCell(transform.position);
        photons.SetTile(pos, photonTile);

        TileBase[] lanternTiles = lanternsTileMap.GetTilesBlock(lanternsTileMap.cellBounds);

        for (int i = 0; i < lanternTiles.Length; i++)
        {
            if (lanternTiles[i] != null)
            {
                lanternCount++;
            }
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int iterations = photonsLeft;
            for (int i = 0; i < iterations; i++)
            {
                Vector3Int pos = photons.WorldToCell(transform.position);
                nextTile = pos + lightRotation.lightDirection * i;

                if (lanternsTileMap.HasTile(nextTile))
                {
                    lanternsTileMap.SetTile(nextTile, litLanternTile);
                    Debug.Log("Player won? " + checkIfPlayerWins());
                }


                if (photons.HasTile(nextTile))
                {
                    iterations++;
                }
                else if (obstacles.HasTile(nextTile))
                {
                    i = iterations;
                }
                else
                {
                    photons.SetTile(nextTile, photonTile);
                    photonsLeft--;
                }
            }
            textMesh.text = "<" + photonsLeft + " Photons Left>";
        }
    }

    private bool checkIfPlayerWins()
    {
        int litLanters = 0;
        TileBase[] lanternTiles = lanternsTileMap.GetTilesBlock(lanternsTileMap.cellBounds);

        for (int i = 0; i < lanternTiles.Length; i++)
        {
            if (lanternTiles[i] == litLanternTile)
            {
                litLanters++;
            }
        }

        if (litLanters == lanternCount) return true;
        else return false;
    }
    public void incrementPhotonsLeft()
    {
        photonsLeft++;
    }
}
