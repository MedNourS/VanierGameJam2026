using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class LanternLightingScript : MonoBehaviour
{
    [SerializeField] private Tilemap lanternsTileMap;
    [SerializeField] private Light2D lanternLightObj;
    [SerializeField] private TileBase litLantern;
    [SerializeField] private float winningLightIntensity;
    [SerializeField] private PlayerLightSystem playerLightSystem;

    private List<TileBase> lanterns;
    private List<Light2D> lanternLights;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int lanternCount = 0;

        TileBase[] lanternTiles = lanternsTileMap.GetTilesBlock(lanternsTileMap.cellBounds);

        for (int i = 0; i < lanternTiles.Length; i++)
        {
            if (lanternTiles[i] != null)
            {
                lanternCount++;
            }
        }

        lanternLights = new List<Light2D>();
        lanterns = new List<TileBase>();

        foreach (var pos in lanternsTileMap.cellBounds.allPositionsWithin)
        {
            Vector3Int tilePos = lanternsTileMap.WorldToCell(pos);
            if (lanternsTileMap.GetTile(tilePos) != null)
            {
                Light2D light = Instantiate(lanternLightObj, tilePos, Quaternion.identity);
                light.intensity = 0;
                lanternLights.Add(light);
                lanterns.Add(lanternsTileMap.GetTile(tilePos));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerLightSystem.playerHasWon)
        {
            for (int i = 0; i < lanterns.Count; i++)
            {
                if (lanterns[i] == litLantern)
                {
                    lanternLights[i].intensity = 1;
                }
                else
                {
                    lanternLights[i].intensity = 0;
                }
            }
        }
    }

    public void updateLanterns()
    {
        lanterns = new List<TileBase>();

        foreach (var pos in lanternsTileMap.cellBounds.allPositionsWithin)
        {
            Vector3Int tilePos = lanternsTileMap.WorldToCell(pos);
            if (lanternsTileMap.GetTile(tilePos) != null)
            {
                lanterns.Add(lanternsTileMap.GetTile(tilePos));
            }
        }
    }

    public void winningLanterns()
    {
        for (int i = 0; i < lanterns.Count; i++)
        {
            lanternLights[i].intensity = winningLightIntensity;
        }
    }
}
