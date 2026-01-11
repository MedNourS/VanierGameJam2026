using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerLightSystem : MonoBehaviour
{
    [SerializeField] private Tilemap photons;
    [SerializeField] private Tilemap colsTileMap;
    [SerializeField] private Tilemap exitTileMap;
    [SerializeField] private Tilemap obstaclesTileMap;
    [SerializeField] private TileBase photonTile;
    [SerializeField] private TileBase litLanternTile;
    [SerializeField] private TileBase bottomObstacle;
    [SerializeField] private TileBase upObstacle;
    [SerializeField] private TileBase leftObstacle;
    [SerializeField] private TileBase rightObstacle;
    [SerializeField] private LightRotation lightRotation;
    [SerializeField] private Tilemap lanternsTileMap;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private LanternLightingScript lanternLightingScript;
    private TilemapCollider2D photonsCollider;
    private Vector3Int nextTile;
    private int lanternCount = 0;
    private Dictionary<TileBase, Vector3Int> tileObstacleDirToVectorDir;
    public int photonsLeft;
    public bool playerHasWon;
    void Start()
    {
        tileObstacleDirToVectorDir = new Dictionary<TileBase, Vector3Int>()
        {
            {bottomObstacle, Vector3Int.down},
            {upObstacle, Vector3Int.up},
            {leftObstacle, Vector3Int.left},
            {rightObstacle, Vector3Int.right},
        };

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
            Vector3Int pos = photons.WorldToCell(transform.position);
            for (int i = 1; i < iterations + 1; i++)
            {
                nextTile = pos + lightRotation.lightDirection * i;

                if (lanternsTileMap.HasTile(nextTile))
                {
                    lanternsTileMap.SetTile(nextTile, litLanternTile);
                    lanternLightingScript.updateLanterns();
                    checkIfPlayerWins();
                }
                //Check for obstacles
                if(!checkForObstacles(pos + lightRotation.lightDirection * (i - 1), lightRotation.lightDirection))
                {
                    i = iterations;
                    continue;
                }
                if (photons.HasTile(nextTile))
                {
                    iterations++;
                }
                else if (colsTileMap.HasTile(nextTile) || exitTileMap.HasTile(nextTile))
                {
                    i = iterations;
                }
                else
                {
                    photons.SetTile(nextTile, photonTile);
                    photonsLeft--;
                }
            }
            PlayerEvents.Singleton.OnPhotonsChanged?.Invoke(this, new PlayerEvents.OnPhotonsChangedEventArgs { photonsLeft = photonsLeft });
        }
    }

    private void checkIfPlayerWins()
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

        if (litLanters == lanternCount)
        {
            playerHasWon = true;
            lanternLightingScript.winningLanterns();
        }
    }
    public bool checkForObstacles(Vector3Int pos, Vector3Int direction)
    {

        //If pos(player or photon) is inside obstacle
        if (obstaclesTileMap.HasTile(pos))
        {
                    Debug.Log("check");
            //Same direction
            if(tileObstacleDirToVectorDir[obstaclesTileMap.GetTile(pos)] == direction) return false;
            return true;
        }
        //If the next tile is obstacle
        else if(obstaclesTileMap.HasTile(pos + direction))
        {
            //Different direction
            if(tileObstacleDirToVectorDir[obstaclesTileMap.GetTile(pos + direction)] == direction * -1) return false;
            return true;
        }
        return true;
    }
    public void incrementPhotonsLeft()
    {
        photonsLeft++;
    }
}
